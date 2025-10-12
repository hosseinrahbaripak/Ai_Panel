//using Ai_Panel.Application.Constants;
//using Ai_Panel.Application.Contracts.Persistence.EfCore;
//using Ai_Panel.Application.DTOs.AiChat;
//using Ai_Panel.Application.Features.AiChat.Request.Command;
//using Ai_Panel.Application.Features.AiChat.Request.Queries;
//using Ai_Panel.Classes;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using PersianAssistant.Extensions;
//using PersianAssistant.Models;
//using System.Net;

//namespace Ai_Panel.Controllers.UserApp
//{
//    [Route("ChatWithAi")]
//    public class ChatWithAiController(IMediator mediator, IUser user, IErrorLog log, IBookPart part, IUserBooks userBooks, WebTools webTools,
//        IAiContentRepository aiContentRepository) : Controller
//    {
//        [Route("Index")]
//        public async Task<IActionResult> Index(string token, int partId)
//        {
//            try
//            {
//                if (string.IsNullOrEmpty(token))
//                    return View("NoPermission");

//                var user1 = await user.GetByToken(token);
//                if (user1 == null || user1.Id == 0)
//                    return View("NoPermission");

//                var userHasAiAccess = await user.Any(x => x.UserId == user1.UserId && x.HasAccessToAiChat);
//                if (!userHasAiAccess)
//                    return View("NoPermission");

//                var bookPart = await part.GetPartById(partId);
//                if (bookPart == null)
//                    return NotFound();

//                var userHasAccessToBook = await userBooks.CheckUserHas(user1.UserId, bookPart.BookId);
//                if (!userHasAccessToBook)
//                    return NotFound();

//                var model = await mediator.Send(new GetUserAiChatHistoryRequest()
//                {
//                    PartId = partId,
//                    UserId = user1.UserId,
//                });

//                if (model.ErrorId != 0)
//                    return View("Error");

//                ViewBag.Token = token;
//                ViewBag.PartId = partId;
//                ViewBag.UserName = user1.Users.FullName;
//                return View((List<AiChatApiDto>)model.Result);
//            }
//            catch (Exception e)
//            {
//                await log.Add(e.Message, e.InnerException?.Message ?? "", "Get History In  AiChatWeb Panel ");
//                return View("Error");
//            }
//        }

//        [Route("Ask")]
//        [HttpPost]
//        public async Task<ActionResult<ServiceMessage>> Ask([FromForm] UserAskFromAiChatApiDto model)
//        {
//            try
//            {
//                //await Task.Delay(2000);
//                //return ResponseManager.FillObject(new AiChatResponseDto()
//                //{
//                //    AiResponse = "من می‌توانم فقط درباره این فصل از کتاب از مجموعه علامه حلی راهنمایی کنم.",
//                //    BookId = 1,
//                //    DateTime = DateTime.UtcNow.AddHours(3.5),
//                //    Id = RandomNumberGenerator.GetInt32(200, 500),
//                //    PartId = model.PartId,
//                //    UserMessage = "سلام",
//                //    UserName = "سعید ایزدی",
//                //});
//                var token = webTools.GetToken();
//                if (string.IsNullOrEmpty(token))
//                    return ResponseManager.DataError(SystemMessages.TokenError);

//                var user1 = await user.GetByToken(token);
//                if (user1 == null || user1.Id == 0)
//                    return ResponseManager.SessionExpire();

//                var userHasAiAccess = await user.Any(x => x.UserId == user1.UserId && x.HasAccessToAiChat);
//                if (!userHasAiAccess)
//                    return ResponseManager.DataError(SystemMessages.UserAiAccessDenied);

//                var bookPart = await part.GetPartById(model.PartId);
//                if (bookPart == null || !bookPart.HasAiContent)
//                    return ResponseManager.DataError(SystemMessages.BookPartNotFound);
//                var userHasAccessToBook = await userBooks.CheckUserHas(user1.UserId, bookPart.BookId);
//                if (!userHasAccessToBook)
//                    return ResponseManager.DataError(SystemMessages.HasNoPermissionForBook);
//                var aiContent = await aiContentRepository.LastOrDefault(x => !x.IsDelete && x.PartId == model.PartId, x => x.OrderBy(o => o.Id), "AiConfig,AiConfig.AiModel,AiConfig.AiModel.Parent");
//                if (aiContent == null)
//                    return ResponseManager.DataError(SystemMessages.AiContentNotFound);
//                var userChatHistory = await mediator.Send(new GetUserAiChatHistoryRequest()
//                {
//                    PartId = model.PartId,
//                    UserId = user1.UserId,
//                });
//                var userChatHistoryModel = (List<AiChatApiDto>)userChatHistory.Result;
//                var chatHistory = new List<ChatHistoryDto>();
//                foreach (var chatModel in userChatHistoryModel)
//                {
//                    chatHistory.Add(new ChatHistoryDto()
//                    {
//                        Role = "user",
//                        Content = chatModel.UserMessage
//                    });
//                    chatHistory.Add(new ChatHistoryDto()
//                    {
//                        Role = "assistant",
//                        Content = chatModel.AiResponse
//                    });
//                }
//                var askHelliGpt = new AskHelliGptDto()
//                {
//                    ai = aiContent.AiConfig.AiModel.Parent.Title,
//                    message = model.Message,
//                    chat_history = chatHistory,
//                    book_content = aiContent.Content,
//                    model = aiContent.AiConfig.AiModel.Title,
//                    prompt = aiContent.AiConfig.Prompt,
//                    n = aiContent.AiConfig.N,
//                    temperature = aiContent.AiConfig.Temperature,
//                    max_tokens = aiContent.AiConfig.MaxTokens,
//                    top_p = aiContent.AiConfig.TopP,
//                    stop = aiContent.AiConfig.Stop,
//                    presence_penalty = aiContent.AiConfig.PresencePenalty,
//                    frequency_penalty = aiContent.AiConfig.FrequencyPenalty
//                };
//                var client = new HttpClient();
//                client.Timeout = TimeSpan.FromSeconds(180);
//                var resApi = await client.PostAsJsonAsync("https://bookai.mhelli.com/v1/question-answer/submit-non-stream", askHelliGpt);
//                var resGpt = new ServiceMessage();
//                if (resApi.StatusCode == HttpStatusCode.OK)
//                {
//                    var resGptStr = await resApi.Content.ReadAsStringAsync();
//                    resGpt = JsonConvert.DeserializeObject<ServiceMessage>(resGptStr);
//                }
//                else
//                {
//                    await log.Add((resApi.ReasonPhrase ?? "") + $"PartId:{model.PartId}&UserId:{user1.UserId}", $"StatusCode:{resApi.StatusCode.ToString()}&UserMsg:{model.Message}", "AskFromBookAi");
//                }
//                AnswerHelliGptDto resultAi = resGpt.ErrorId >= 0 ? resGpt.Result.ToObject<AnswerHelliGptDto>() : null;
//                var res = await mediator.Send(new AskFromAiRequest()
//                {
//                    Model = new UpsertUserAiChatLogDto()
//                    {
//                        PartId = model.PartId,
//                        BookId = bookPart.BookId,
//                        UserId = user1.UserId,
//                        UserMessage = model.Message,
//                        AiCouldResponse = (resApi.StatusCode == HttpStatusCode.OK && resGpt.ErrorId >= 0),
//                        AiResponse = resGpt.ErrorId == 0 ? string.Join(" | ", resultAi.response) : resGpt.ErrorTitle,
//                        SummarizationCost = resultAi?.Cost?.Summarization_cost ?? 0,
//                        RequestCost = resultAi?.Cost?.Request_cost ?? 0,
//                        EmbeddingCost = resultAi?.Cost?.Embedding_cost ?? 0,
//                        DateTime = DateTime.UtcNow.AddHours(3.5),
//                        User = user1.Users,
//                        AiConfigId = aiContent.AiConfigId
//                    },
//                    GetFullData = true
//                });
//                return res;
//            }
//            catch (Exception e)
//            {
//                await log.Add(e.Message, e.InnerException?.Message ?? "", "User Ask From Ai Chat Api In Api UserLikeOnBookPart");
//                return ResponseManager.ServerError();
//            }
//        }
//    }
//}
