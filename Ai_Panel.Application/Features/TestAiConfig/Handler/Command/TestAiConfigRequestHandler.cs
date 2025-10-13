using AutoMapper;
using Ai_Panel.Application.Constants;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs.AiChat;
using Ai_Panel.Application.Features.TestAiConfig.Request.Command;
using Ai_Panel.Domain;
using MediatR;
using PersianAssistant.Extensions;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.TestAiConfig.Handler.Command;

public class TestAiConfigRequestHandler : IRequestHandler<TestAiConfigRequest, ServiceMessage>
{
    private readonly IMapper _mapper;

    //public TestAiConfigRequestHandler(IMapper mapper, ITestAiConfigRepository testAiConfigRepository, IAdminManage adminManage)
    //{
	   // _mapper = mapper;
	   // _testAiConfigRepository = testAiConfigRepository;
	   // _adminManage = adminManage;
    //}
    public async Task<ServiceMessage> Handle(TestAiConfigRequest request, CancellationToken cancellationToken)
    {

		return ResponseManager.FillObject(new AiChatResponseDto());
 //       var model = _mapper.Map<Domain.TestAiConfig>(request.Model);
 //       //model.BookId = model.BookId > 0 ? model.BookId : null;
 //       //model.PartId = model.PartId > 0 ? model.PartId : null;

		//	var admin = await _adminManage.FirstOrDefault(x => x.LoginID == model.AdminId);
		//       if (model.Id == 0)
		//       {
		//           await _testAiConfigRepository.Add(model);
		//       }
		//       else
		//       {
		//           await _testAiConfigRepository.Update(model);
		//       }
		//	return ResponseManager.FillObject(new AiChatResponseDto()
		//	{
		//		AiResponse = model.AiResponse,
		//		//BookId = model.BookId,
		//		DateTime = model.DateTime,
		//		Id = model.Id,
		//		//PartId = model.PartId,
		//		UserMessage = model.Message,
		//		UserName = admin.UserName,
		//	});
	}
}