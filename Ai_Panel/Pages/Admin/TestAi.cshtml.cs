using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs.AiChat;
using Ai_Panel.Application.Services.Ai;
using Ai_Panel.Infrastructure.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ai_Panel.Pages.Admin
{
    [PermissionChecker]
    public class TestAiModel : PageModel
    {
        private readonly IAiModelRepository _aiModelRepository;

        public TestAiModel(IAiModelRepository aiModelRepository)
        {
            _aiModelRepository = aiModelRepository;
        }

        [BindProperty]
        public TestModel testModel { get; set; }
        public List<string> UniqueOwners { get; set; } = new();
        public async Task OnGetAsync()
        {
            var url = "https://api.avalai.ir/public/models";

            using var client = new HttpClient();
            var response = await client.GetStringAsync(url);

            List<Domain.AiModel> AiModels = new List<Domain.AiModel>();

            // تبدیل به JObject
            var json = JObject.Parse(response);


            // گرفتن آرایه data
            var dataArray = json["data"] as JArray;

            if (dataArray != null)
            {
                // گرفتن همه owned_by ها و یونیک کردن
                UniqueOwners = dataArray
                    .Select(x => x["owned_by"]?.ToString())
                    .Where(x => x != null)
                    .Distinct()
                    .ToList();
            }

            List<Domain.AiModel> aiModels = await _aiModelRepository.GetAll();

            foreach (var model in dataArray)
            {
                var parent = aiModels.FirstOrDefault(x =>
                    string.Equals(x.Title, model["owned_by"]?.ToString(), StringComparison.OrdinalIgnoreCase));

                if (parent == null) 
                {
                    Console.WriteLine("ss");
                }
                else if (parent != null)
                {
                    // مقدار پیش‌فرض صفر در صورت عدم وجود کلید
                    double cachedInput = model["pricing"]?["cached_input"]?.Value<double>() ?? 0;
                    double inputPrice = model["pricing"]?["input"]?.Value<double>() ?? 0;
                    double outputPrice = model["pricing"]?["output"]?.Value<double>() ?? 0;

                    var dbModel = new Domain.AiModel
                    {
                        Title = model["id"]?.ToString() ?? "Unknown",
                        ParentId = parent.Id,
                        CachedInputPrice = cachedInput,
                        InputPrice = inputPrice,
                        OutputPrice = outputPrice,
                        IsDelete = false
                    };

                    try
                    {
                        //await _aiModelRepository.Add(dbModel);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error adding model {dbModel.Title}: {e}");
                    }
                }




            }




            //foreach (var item in UniqueOwners)
            //{
            //    var model = new Domain.AiModel()
            //    {
            //        CachedInputPrice = null,
            //        InputPrice = null,
            //        IsDelete = false,
            //        Title = item
            //    };
            //    await _aiModelRepository.Add(model);
            //    //AiModels.Add(new Domain.AiModel() {
            //    //CachedInputPrice=null,
            //    //InputPrice = null , 
            //    //IsDelete = false , 
            //    //Title = item
            //    //});
            //}

        }

        public string AiResponse { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(testModel.Prompt) && string.IsNullOrEmpty(testModel.Message))
            {
                AiResponse = "Please provide a prompt!";
                return Page();
            }

            ChatCompletionDto chatCompletionDetail = new ChatCompletionDto()
            {
                Prompt = testModel.Prompt,
                Message = testModel.Message,
                BaseUrl = "https://api.avalai.ir/v1/chat/completions",
                Model = "gpt-4o"
            };
            // کال API
            //var res = await _aiApiClient.GetChatCompletionAsync(chatCompletionDetail);
            //AiResponse = res.Result;

            return Page();
        }
    }
}
public class TestModel
{
    public string Prompt { get; set; }
    public string Message { get; set; }
}