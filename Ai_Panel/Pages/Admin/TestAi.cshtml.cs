using Ai_Panel.Application.Services.Ai;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Panel.Pages.Admin
{
    [Authorize]
    public class TestAiModel : PageModel
    {
        private readonly IAiApiClient _aiApiClient;

        // Inject کردن سرویس از طریق constructor
        public TestAiModel(IAiApiClient aiApiClient)
        {
            _aiApiClient = aiApiClient;
        }

        public void OnGet()
        {
        }

        public string? AiResponse { get; set; }

        public async Task<IActionResult> OnPostAsync(string prompt)
        {
            if (string.IsNullOrEmpty(prompt))
            {
                AiResponse = "Please provide a prompt!";
                return Page();
            }

            // کال API
            AiResponse = await _aiApiClient.GetChatCompletionAsync(prompt);

            return Page();
        }
    }
}
