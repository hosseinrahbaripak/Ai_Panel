using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.Features.AiServices.Request.Command;
using Ai_Panel.Domain;
using MediatR;

namespace Ai_Panel.Application.Features.AiServices.Handler.Command
{
    public class UpserAiServiceHandler : IRequestHandler<UpserAiServiceRequest, Unit>
    {
        private readonly IGenericRepository<AiService> _aiService;
        public UpserAiServiceHandler(IGenericRepository<AiService> aiService) {
            _aiService = aiService;
        }
        public async Task<Unit> Handle(UpserAiServiceRequest request, CancellationToken cancellationToken)
        {
            if(request.AiService.Id == 0)
            {
                AiService aiService = new AiService()
                {
                    Title = request.AiService.Title,
                    Description = request.AiService.Description,
                    AiConfigGroupId = request.AiService.AiConfigGroupId,
                    IsActive = request.AiService.IsActive,
                    IsRecommended = request.AiService.IsRecommended,
                    UpdateDateTime = DateTime.Now.AddHours(3.5),
                    DateTime = DateTime.Now.AddHours(3.5),
                };
                await _aiService.Add(aiService);
                return Unit.Value;
            }
            var model = await _aiService.Get(request.AiService.Id);
            model.Title = request.AiService.Title;
            model.Description = request.AiService.Description;
            model.IsActive = request.AiService.IsActive;
            model.AiConfigGroupId = request.AiService.AiConfigGroupId;
            model.UpdateDateTime = DateTime.UtcNow.AddHours(3.5);
            model.IsRecommended = request.AiService.IsRecommended;
            await _aiService.Update(model);
            return Unit.Value;



        }
    }
}
