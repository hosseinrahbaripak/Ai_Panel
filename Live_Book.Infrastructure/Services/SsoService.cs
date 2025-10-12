using Live_Book.Infrastructure.Contracts;
using Live_Book.Infrastructure.Models.Sso;
using Live_Book.Infrastructure.Tools;
using MHelliClass.Infrastructure.Model.Sso;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Live_Book.Infrastructure.Services;
public class SsoService(IMemoryCache memoryCache, IConfiguration configuration) : ISsoService
{
	public async Task<string> GetToken()
	{
		var tokenInCache = memoryCache.Get<string>("SsoApiToken");
		if (!string.IsNullOrEmpty(tokenInCache))
			return tokenInCache;
		var reqDto = new GetProjectTokenRequestDto()
		{
			ApiKey = configuration["SsoApi:ApiKey"],
			SecretKey = configuration["SsoApi:SecretKey"],
			Title = configuration["SsoApi:Title"],
			ClientId = configuration["SsoApi:ClientId"],
		};
		var apiClient = new ApiClient(configuration["SsoApi:BaseUrl"] + configuration["SsoApi:GetTokenUrl"], "post");
		apiClient.Headers.Add("ClientId", configuration["SsoApi:ClientId"]);
		apiClient.Body = reqDto;
		var apiRes = await apiClient.Send<ApiResponse<GetProjectTokenResponseDto>>();
		if (!apiRes.IsSuccess) return null;
		if (apiRes.Data == null || apiRes.Data.ErrorId < 0) return null;
		memoryCache.Set("SsoApiToken", apiRes.Data.Result.Token, DateTime.Now.AddMinutes(58));
		return apiRes.Data.Result.Token;
	}

	public async Task<bool> ValidateToken(string token)
	{
        var reqDto = new ValidateTokenRequestDto()
        {
            ApiKey = configuration["SsoApi:ApiKey"],
            SecretKey = configuration["SsoApi:SecretKey"],
            Title = configuration["SsoApi:Title"],
            ClientId = configuration["SsoApi:ClientId"],
            Token = token
        };
        var apiClient = new ApiClient(configuration["SsoApi:BaseUrl"] + configuration["SsoApi:IsTokenValidUrl"], "post");
        apiClient.Headers.Add("ClientId", configuration["SsoApi:ClientId"]);
        apiClient.Body = reqDto;
        var apiRes = await apiClient.Send<ApiResponse<ValidateTokenResponseDto>>();
        if (!apiRes.IsSuccess || apiRes.Data == null) return false;
        return apiRes.Data.ErrorId == 0;
    }
}
