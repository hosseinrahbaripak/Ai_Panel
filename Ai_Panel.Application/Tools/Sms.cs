using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Ai_Panel.Application.Tools;

public class Sms(IConfiguration configuration)
{
    public async Task SendSms(string phoneNumber, string message, string browser)
    {
        var url = configuration["Sms:Url"];
        if (url != null)
        {
            if (url.Contains("sms.ir"))
            {
                await SendIrSms(url, phoneNumber, message);
            }
            else
            {
                await SendSms(url, phoneNumber, message, browser);
            }
        } 
    }
    public async Task SendSms(string phoneNumber, List<Tuple<string, string>> message, string tmp)
    {
        var url = configuration["Sms:Url"];
        if (url != null)
        {
            if (url.Contains("sms.ir"))
            {
                //await SendIrSms(url, phoneNumber, message);
            }
            else
            {
                await SendSms(url, phoneNumber, message, tmp);
            }
        } 
    }
    private async Task SendIrSms(string url,string phoneNumber, string message)
    {
        var tmp = configuration["Sms:Template"];
        var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("x-api-key", configuration["Sms:Key"]);

        if (tmp != null)
        {
            var model = new VerifySendModel()
            {
                Mobile = phoneNumber,
                TemplateId = int.Parse(tmp),
                Parameters =
                [
                    new VerifySendParameterModel {
                        Name = "CODE", Value = message
                    }
                ]
            };

            var payload = JsonSerializer.Serialize(model);
            StringContent stringContent = new(payload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, stringContent);
        }
    }
    private async Task SendSms(string url,string phoneNumber, string message, string browser)
    {
        var tmp = configuration["Sms:Template"];
        if (browser.ToLower().Contains("android")) tmp = configuration["Sms:TemplateByLink"];

        var client = new RestClient(url);
        var request = new RestRequest
        {
            Method = Method.Post
        };
        request.AddHeader("apikey", configuration["Sms:Key"]);
        request.AddParameter("type", 1);
        request.AddParameter("receptor", phoneNumber);
        request.AddParameter("template", tmp);
        request.AddParameter("param1", message);
        var response = await client.ExecuteAsync(request);
    }

    private async Task SendSms(string url, string phoneNumber, List<Tuple<string, string>> message, string tmp)
    {
        var client = new RestClient(configuration["Sms:Url"]);
        var request = new RestRequest
        {
            Method = Method.Post
        };
        request.AddHeader("apikey", configuration["Sms:Key"]);
        request.AddParameter("type", 1);
        request.AddParameter("receptor", phoneNumber);
        request.AddParameter("template", tmp);
        foreach (var item in message) request.AddParameter(item.Item1, item.Item2);
        var response = await client.ExecuteAsync(request);
    }

    #region --Models--
    public class VerifySendParameterModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class VerifySendModel
    {
        public string Mobile { get; set; }

        public int TemplateId { get; set; }

        public VerifySendParameterModel[] Parameters { get; set; }
    }

    #endregion
}