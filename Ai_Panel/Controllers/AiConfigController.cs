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

        [Authorize]
        [Route("Edit")]
        [HttpPost]
        public async Task<ActionResult<ServiceMessage>> Edit(List<EditAiCofigListDto> model)
        {
            if (model == null || model.Count < 1)
            {
                return new ServiceMessage()
                {
                    ErrorId = -1,
                    ErrorTitle = "حداقل یک کانفیگ باید اضافه کنید",
                    Result = null
                };

            }
            List<AiConfig> AiConfigs = await _aiConfig.GetAll(where: conf => !conf.IsDelete && conf.AiConfigGroupId == model.First().GroupId);
            var postedIds = model.Where(x => x.Id.HasValue).Select(x => x.Id.Value).ToList();

            var toDelete = AiConfigs.Where(x => !postedIds.Contains(x.Id)).ToList();
            foreach (var delItem in toDelete)
            {
                delItem.IsDelete = true;
                delItem.UpdateDateTime = DateTime.UtcNow.AddHours(3.5);
                await _aiConfig.Update(delItem);
            }
            var toUpdate = model.Where(x => x.Id.HasValue).ToList();
            foreach (var dto in toUpdate)
            {
                var dbItem = AiConfigs.First(x => x.Id == dto.Id.Value);

                dbItem.Title = dto.Title;
                dbItem.AiPlatformId = dto.AiPlatformId;
                dbItem.AiModelId = dto.AiModelId;
                dbItem.Temperature = dto.Temperature;
                dbItem.PresencePenalty = dto.PresencePenalty;
                dbItem.TopP = dto.TopP;
                dbItem.FrequencyPenalty = dto.FrequencyPenalty;
                dbItem.MaxTokens = dto.MaxTokens;
                dbItem.Stop = dto.Stop;
                dbItem.Prompt = dto.Prompt;
                dbItem.AiConfigOrder = dto.AiConfigOrder;
                dbItem.UpdateDateTime = DateTime.UtcNow.AddHours(3.5);
                await _aiConfig.Update(dbItem);
            }
            var toAdd = model.Where(x => !x.Id.HasValue).Select(dto => new AiConfig
            {
                AiConfigGroupId = dto.GroupId.Value,
                Title = dto.Title,
                AiPlatformId = dto.AiPlatformId,
                AiModelId = dto.AiModelId,
                Temperature = dto.Temperature,
                PresencePenalty = dto.PresencePenalty,
                TopP = dto.TopP,
                FrequencyPenalty = dto.FrequencyPenalty,
                MaxTokens = dto.MaxTokens,
                Stop = dto.Stop,
                Prompt = dto.Prompt,
                AiConfigOrder = dto.AiConfigOrder,
                IsDelete = false,
                Version="1.0.1",
                DateTime=DateTime.UtcNow.AddHours(3.5)
            }).ToList();

             foreach(var item in toAdd)
            {
                await _aiConfig.Add(item);
            }
            return new ServiceMessage()
            {
                ErrorId = 0,
                Result = "مدل ها ویرایش شدند",
                ErrorTitle = null

            };


        }

    }
}
