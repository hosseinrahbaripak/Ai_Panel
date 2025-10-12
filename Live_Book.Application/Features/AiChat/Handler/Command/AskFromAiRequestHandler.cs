using AutoMapper;
using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Application.DTOs.AiChat;
using Live_Book.Application.Features.AiChat.Request.Command;
using Live_Book.Domain;
using MediatR;
using PersianAssistant.Extensions;
using PersianAssistant.Models;

namespace Live_Book.Application.Features.AiChat.Handler.Command;

public class AskFromAiRequestHandler : IRequestHandler<AskFromAiRequest, ServiceMessage>
{
    private readonly IUserAiChatLogsRepository _userAiChatLogsRepository;
    private readonly IMapper _mapper;

    public AskFromAiRequestHandler(IUserAiChatLogsRepository userAiChatLogsRepository, IMapper mapper)
    {
        _userAiChatLogsRepository = userAiChatLogsRepository;
        _mapper = mapper;
    }
    public async Task<ServiceMessage> Handle(AskFromAiRequest request, CancellationToken cancellationToken)
    {
        return ResponseManager.DataError("با عرض پورزش فعلا قادر به پاسخگویی نیستیم !");
        //var model = _mapper.Map<UserAiChatLog>(request.Model);
        //if (model == null)
        //{
        //    return ResponseManager.DataError("خطا در ثبت اطلاعات !");
        //}
        //var user = model.User;
        //model.User = null;
        //if (model.Id == 0)
        //{
        //    await _userAiChatLogsRepository.Add(model);
        //}
        ////else
        ////{
        ////    await _userAiChatLogsRepository.Update(model);
        ////}
        //if (model.AiCouldResponse)
        //{
        //    if (!request.GetFullData)
        //        return ResponseManager.FillObject(model.AiResponse ?? "");
        //    else
        //    {
        //        return ResponseManager.FillObject(new AiChatResponseDto()
        //        {
        //            AiResponse = model.AiResponse,
        //            BookId = model.BookId,
        //            DateTime = model.DateTime,
        //            Id = model.Id,
        //            PartId = model.PartId,
        //            QuestionId = model.QuestionId,
        //            UserMessage = model.UserMessage,
        //            UserName = user?.FullName ?? "",
        //        });
        //    }
        //}
        //else
        //{
        //    return ResponseManager.DataError("با عرض پورزش فعلا قادر به پاسخگویی نیستیم !");
        //}
    }
}