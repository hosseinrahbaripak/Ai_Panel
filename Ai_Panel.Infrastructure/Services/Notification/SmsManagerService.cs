using Ai_Panel.Infrastructure.Contracts.Notification;
using Ai_Panel.Infrastructure.Tools;
using Microsoft.Extensions.Configuration;

namespace Ai_Panel.Infrastructure.Services.Notification;
public class SmsManagerService(IConfiguration configuration) : ISmsManagerService
{
    public async Task<bool> Login(string mobile, string code)
    {
        var tempId = configuration["Notification:SMS:Templates:Login"];
        var requestData = new
        {
            TempId = tempId,
            DestinationNumber = mobile,
            Parameters = new
            {
                VerificationCode = code,
            }
        };
        return await SendMessage(requestData, mobile);
    }
    public async Task<bool> PaymentSuccess(string mobile, string name, string amount)
    {
        var tempId = configuration["Notification:SMS:Templates:PaymentSuccess"];
        var requestData = new
        {
            TempId = tempId,
            DestinationNumber = mobile,
            Parameters = new
            {
                Name = name,
                Amount = amount
            }
        };
        return await SendMessage(requestData, mobile);

    }
    public async Task<bool> SendMessage(object payLoad, string destination)
    {
        var logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");
        var logFileName = "mhellismslog.txt";
        try
        {
            var url = configuration["Notification:SMS:Url"];
            var title = configuration["Notification:Title"];
            var token = configuration["Notification:Token"];
            var apiClient = new ApiClient(url, "post");
            apiClient.Body = payLoad;
            apiClient.Headers.Add("title", title);
            apiClient.Headers.Add("token", token);
            var res = await apiClient.Send<ApiResponse>();
            if (res.IsSuccess)
            {
                var data = res.Data;
                if (data?.ErrorId < 0)
                {
                    await StaticFileHelper.WriteOnTextFile(logFolderPath, logFileName, data.ErrorTitle + " " + data.ErrorId + " : " + destination);
                    return false;
                }
                return true;
            }
            else
            {
                await StaticFileHelper.WriteOnTextFile(logFolderPath, logFileName, res.ErrorMessage + " " + res.StatusCode + " : " + destination);
                return false;
            }
        }
        catch (Exception e)
        {
            StaticFileHelper.WriteOnTextFile(logFolderPath, logFileName, e.Message + destination + " : " + DateTime.UtcNow.AddHours(3.5)).Wait();
            return false;
        }
    }
}
