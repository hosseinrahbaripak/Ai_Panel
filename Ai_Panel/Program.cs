using System.Text;
using Ai_Panel.Application.Configurations;
using Ai_Panel.Application.Services.Ai;
using Ai_Panel.Application.Tools;
using Ai_Panel.Classes;
using Ai_Panel.Infrastructure.Configurations;
using Ai_Panel.Persistence.Configurations;
using AspNetCoreRateLimit;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container. 
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        builder =>
        {
            builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
        });
});

builder.Services.AddHttpClient<IAiApiClient, AiApiClient>();

builder.Services.AddRazorPages();
//builder.Services.AddControllersWithViews();
//builder.Services.AddMvc(options =>
//{
//    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
//});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
    x.MultipartHeadersLengthLimit = int.MaxValue;
});
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue;
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AiPanel", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "لطفا JWT را به صورت 'Bearer {token}' وارد کنید"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices();
#region Ip Rate Limit Services
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddControllers();
#endregion

#region IOC
builder.Services.AddSingleton<SettingMethod>();
builder.Services.AddScoped<PaymentHelper>();
builder.Services.AddSingleton<WebTools>();
#endregion

#region Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(option =>
        {
            option.LoginPath = "/Login";
            option.LogoutPath = "/SignOut";
            option.ExpireTimeSpan = TimeSpan.FromDays(3);
            option.Cookie.HttpOnly = true;
        }).AddJwtBearer("JwtBearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
            };
        });
#endregion


#region --Captcha--
builder.Services.AddDNTCaptcha(options =>
{
    options.UseCookieStorageProvider(SameSiteMode.Strict /* If you are using CORS, set it to `None` */) // -> It relies on the server and client's times. It's ideal for scalability, because it doesn't save anything in the server's memory.

        .AbsoluteExpiration(minutes: 2)
        .ShowThousandsSeparators(false)
        .WithNoise(0.015f, 0.015f, 1, 0.0f)
        .WithEncryptionKey("This is my secure key!")
        .InputNames(// This is optional. Change it if you don't like the default names.
            new DNTCaptchaComponent
            {
                CaptchaHiddenInputName = "DNT_CaptchaText",
                CaptchaHiddenTokenName = "DNT_CaptchaToken",
                CaptchaInputName = "DNT_CaptchaInputText"
            })
        .Identifier("dnt_Captcha");// This is optional. Change it if you don't like its default name.
});
#endregion
var app = builder.Build();

#region UnderConstruction
var goUnderConstruction = builder.Configuration["Url:UnderConstruction"];
if (goUnderConstruction?.ToLower() == "true")
{
    app.Use(async (context, next) =>
    {
        if (context.Request.Path.Value != "/UnderConstruction")
        {
            context.Response.Redirect("/UnderConstruction");
        }
        await next();
    });
}
#endregion

#region Error 404
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Response.Redirect("/Home/Error404");
    }
});
#endregion

#region ServerFileLock
app.Use(async (context, next) =>
{
    string pathValue = context?.Request?.Path.Value.ToString().ToLower();
    if (!string.IsNullOrEmpty(pathValue) && (pathValue.StartsWith("/files/book/pdf")))
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context?.Response.Redirect("/Error404");
        }
        else
        {
            await next.Invoke();
        }
    }
    else
    {
        await next.Invoke();
    }
});
#endregion

#region Prevent DDos Attack
app.UseIpRateLimiting();
#endregion

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=AdminHome}/{action=AdminIndex}/{id?}");

app.Run();