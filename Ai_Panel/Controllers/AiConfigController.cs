using System.Security.Claims;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs.AiConfig;
using Ai_Panel.Application.DTOs.User;
using Ai_Panel.Application.Tools;
using Ai_Panel.Domain;
using Ai_Panel.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersianAssistant.Models;

namespace Ai_Panel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiConfigController : Controller
    {
        private readonly IGenericRepository<AiConfig> _aiConfig;
        private readonly IGenericRepository<AiConfigGroup> _aiConfigGroup;

        public AiConfigController(IGenericRepository<AiConfig> aiConfig , IGenericRepository<AiConfigGroup> aiConfigGroup)
        {
            _aiConfig = aiConfig;
            _aiConfigGroup = aiConfigGroup;
        }

        [Authorize]
        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult<ServiceMessage>> Add(List<AddAiConfigListDto> model) {

            if(model == null || model.Count < 1)
            {
                return new ServiceMessage()
                {
                    ErrorId = -1 , 
                    ErrorTitle = "حداقل یک کانفیگ باید اضافه کنید",
                    Result = null
                };

            }
            AiConfigGroup group = new AiConfigGroup() {
            IsDelete = false,
            };

            var AddedGroup = await _aiConfigGroup.Add(group);
            var userId = User.FindFirstValue(CustomClaimTypes.UserId);

            foreach (var config in model)
            {
                var conf = new AiConfig()
                {
                    AiConfigGroupId = AddedGroup.Id,
                    DateTime = DateTime.UtcNow.AddHours(3.5),
                    MaxTokens = config.MaxTokens,
                    FrequencyPenalty = (float)config.FrequencyPenalty,
                    Prompt = config.Prompt,
                    AiPlatformId = (int)config.AiPlatformId,
                    AiModelId = (int)config.AiModelId,
                    N = 1,
                    PresencePenalty = config.PresencePenalty,
                    IsDelete = false,
                    Stop = config.Stop,
                    Temperature = config.Temperature,
                    TopP = config.TopP,
                    AiConfigOrder = (int)config.AiConfigOrder,
                    Title = config.Title,
                    UpdateDateTime = DateTime.UtcNow.AddHours(3.5),
                    Version="1.0.1"
                };
                await _aiConfig.Add(conf);
            }

            return new ServiceMessage()
            {
                ErrorId = 0,
                Result = "مدل ها اضافه شدند",
                ErrorTitle = null

            };
                
                
        }
        
    }
}
