using Ai_Panel.Application.DTOs.AiChat;
using Ai_Panel.Application.Services.Ai;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace Ai_Panel.Pages.Admin
{
    [Authorize]
    public class TestAiModel : PageModel
    {
        private readonly IAiApiClient _aiApiClient;
        public TestAiModel(IAiApiClient aiApiClient)
        {
            _aiApiClient = aiApiClient;
        }

        [BindProperty]
        public TestModel testModel { get; set; }

        public void OnGet()
        {
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
            var res = await _aiApiClient.GetChatCompletionAsync(chatCompletionDetail);
            AiResponse = res.Result;

            return Page();
        }
    }
}
public class TestModel
{
    public string Prompt { get; set; }
    public string Message { get; set; }
}