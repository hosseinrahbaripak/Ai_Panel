using AutoMapper;
using Live_Book.Application.Constants;
using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Application.DTOs.AiChat;
using Live_Book.Application.Features.TestAiConfig.Request.Command;
using Live_Book.Domain;
using MediatR;
using PersianAssistant.Extensions;
using PersianAssistant.Models;

namespace Live_Book.Application.Features.TestAiConfig.Handler.Command;

public class TestAiConfigRequestHandler : IRequestHandler<TestAiConfigRequest, ServiceMessage>
{
    private readonly IMapper _mapper;
    private readonly ITestAiConfigRepository _testAiConfigRepository;
    private readonly IAdminManage _adminManage;

    public TestAiConfigRequestHandler(IMapper mapper, ITestAiConfigRepository testAiConfigRepository, IAdminManage adminManage)
    {
	    _mapper = mapper;
	    _testAiConfigRepository = testAiConfigRepository;
	    _adminManage = adminManage;
    }
    public async Task<ServiceMessage> Handle(TestAiConfigRequest request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<Domain.TestAiConfig>(request.Model);
        //model.BookId = model.BookId > 0 ? model.BookId : null;
        //model.PartId = model.PartId > 0 ? model.PartId : null;

		var admin = await _adminManage.FirstOrDefault(x => x.LoginID == model.AdminId);
        if (model.Id == 0)
        {
            await _testAiConfigRepository.Add(model);
        }
        else
        {
            await _testAiConfigRepository.Update(model);
        }
		return ResponseManager.FillObject(new AiChatResponseDto()
		{
			AiResponse = model.AiResponse,
			//BookId = model.BookId,
			DateTime = model.DateTime,
			Id = model.Id,
			//PartId = model.PartId,
			UserMessage = model.Message,
			UserName = admin.UserName,
		});
	}
}