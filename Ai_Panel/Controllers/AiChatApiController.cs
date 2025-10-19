using Ai_Panel.Application.Constants;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs;
using Ai_Panel.Application.DTOs.AiChat;
using Ai_Panel.Application.Services.Ai;
using Ai_Panel.Classes;
using Ai_Panel.Models;
using Ai_Panel.Pages.Admin;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersianAssistant.Models;

namespace Ai_Panel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "JwtBearer")]
    public class AiChatApiController(
        IMediator mediator, WebTools webTools, IErrorLog log, IUser user,
        IMapper mapper, IAiPlatformRepository aiPlatform, IAiApiClient aiApiClient, IAiConfigRepository aiConfig, IGenericRepository<Domain.TestAiConfig> testAiConfig) : ControllerBase
    {
        [Route("Ask")]
        [HttpPost]
        public async Task<ActionResult<ServiceMessage>> Ask(UserAskAiDto model)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.UserId)?.Value;
            var AIPlatform = await aiPlatform.FirstOrDefault(where: p => p.Id == model.AiPlatformId);
            if (AIPlatform == null)
            {
                return new ServiceMessage()
                {
                    ErrorId = -1,
                    ErrorTitle = "پلتفرم انتخاب شده یافت نشد",
                    Result = null
                };
            }
            try
            {
                string[] StopArr = model.StopStr.Split(',', StringSplitOptions.RemoveEmptyEntries);
                ChatCompletionDto chatCompletionDetail = new ChatCompletionDto()
                {
                    Prompt = model.Prompt,
                    Message = model.Message,
                    BaseUrl = AIPlatform.ApiEndpoint,
                    Model = model.AiModel,
                    MaxTokens = model.MaxTokens,
                    FrequencyPenalty = model.FrequencyPenalty,
                    PresencePenalty = model.PresencePenalty,
                    Stop = StopArr,
                    Temperature = model.Temperature,
                    TopP = model.TopP,
                    N = 1
                };
                var res = await aiApiClient.GetChatCompletionAsync(chatCompletionDetail);
                if (res.ErrorId == 0)
                {
                    var TestAiConfigModel = new Domain.TestAiConfig()
                    {
                        AiResponse = res.Result.AiResponse,
                        DateTime = DateTime.UtcNow.AddHours(3.5),
                        userId = int.Parse(userIdClaim),
                        RequestCost = res.Result.RequestCost,
                        AiModelId = model.AiModelId,
                        FrequencyPenalty = model.FrequencyPenalty,
                        PresencePenalty = model.PresencePenalty,
                        Stop = StopArr,
                        MaxTokens = model.MaxTokens,
                        Prompt = model.Prompt,
                        Message = model.Message,
                        N = 1,
                        IsDelete = false,
                        SummarizationCost = 0,
                        EmbeddingCost = 0,
                        Temperature = model.Temperature,
                        TopP = model.TopP,
                        UpdateDateTime = DateTime.UtcNow.AddHours(3.5),
                    };
                    await testAiConfig.Add(TestAiConfigModel);
                }
                return new ServiceMessage()
                {
                    Result = res.Result.AiResponse,
                    ErrorTitle = null,
                    ErrorId = 0
                };
            }
            catch (Exception e)
            {
                return new ServiceMessage()
                {
                    ErrorId = -1,
                    ErrorTitle = e.Message,
                    Result = null
                };
            }

        }
        [Route("delete")]
        [HttpPost]
        public async Task<ActionResult<ServiceMessage>> DeleteConfig(DeleteRequest request)
        {
            var model = await aiConfig.FirstOrDefault(where: c => c.Id == request.Id && !c.IsDelete);
            if (model == null)
            {
                return new ServiceMessage()
                {
                    ErrorId = -1,
                    ErrorTitle = SystemMessages.AiConfigNotFound,
                    Result = null
                };
            }
            model.IsDelete = true;
            await aiConfig.Update(model);
            return new ServiceMessage()
            {
                ErrorId = 0,
                ErrorTitle = null,
                Result = SystemMessages.Success
            };
        }
        //{
        //    try
        //    {
        //        var token = webTools.GetToken();
        //        if (string.IsNullOrEmpty(token))
        //            return ResponseManager.DataError(SystemMessages.TokenError);

        //        var user1 = await user.GetByToken(token);
        //        if (user1 == null || user1.Id == 0)
        //            return ResponseManager.SessionExpire();

        //        var userHasAiAccess = await user.Any(x => x.UserId == user1.UserId && x.HasAccessToAiChat);
        //        if (!userHasAiAccess)
        //            return ResponseManager.DataError(SystemMessages.UserAiAccessDenied);

        //        var bookPart = await part.GetPartById(model.PartId);
        //        if (bookPart == null || !bookPart.HasAiContent)
        //            return ResponseManager.DataError(SystemMessages.BookPartNotFound);
        //        var userHasAccessToBook = await userBooks.CheckUserHas(user1.UserId, bookPart.BookId);
        //        if (!userHasAccessToBook)
        //            return ResponseManager.DataError(SystemMessages.HasNoPermissionForBook);
        //        var promptDictionary = new Dictionary<string, string>();
        //        promptDictionary["Book"] = bookPart?.Book?.Name ?? "";
        //        promptDictionary["Part"] = bookPart?.PartName ?? "";
        //        promptDictionary["User"] = user1.Users.FullName ?? "";
        //        AiConfig config;
        //        var content = "";
        //        BookQuestion question = null;
        //        if (model.QuestionId != null)
        //        {
        //            question = await bookQuestion.FirstOrDefault(x => x.Id == model.QuestionId, includeProperties: "AiConfig,AiConfig.AiModel,AiConfig.AiModel.Parent,AiConfig.AiPlatform");
        //            if (question == null)
        //                return ResponseManager.DataError(SystemMessages.QuestionNotFound);
        //            if (question.PartId != model.PartId)
        //                return ResponseManager.DataError(SystemMessages.QuestionInBookPartNotFound);
        //            if (question.AiConfigId == null)
        //                return ResponseManager.DataError(SystemMessages.QuestionNotHaveAiConfig);
        //            config = question.AiConfig;
        //            content = question.Content + question.Question + question.Answer;
        //            promptDictionary["Question"] = question.Question;
        //            promptDictionary["Answer"] = question.Answer;
        //            promptDictionary["Content"] = question.Content ?? "";
        //        }
        //        else
        //        {
        //            var aiContent = await aiContentRepository.LastOrDefault(x => !x.IsDelete && x.PartId == model.PartId, x => x.OrderBy(o => o.Id), "AiConfig,AiConfig.AiModel,AiConfig.AiModel.Parent,AiConfig.AiPlatform");
        //            if (aiContent == null)
        //                return ResponseManager.DataError(SystemMessages.AiContentNotFound);
        //            config = aiContent.AiConfig;
        //            content = aiContent.Content;
        //        }
        //        var userChatHistory = await mediator.Send(new GetUserAiChatHistoryRequest()
        //        {
        //            PartId = model.PartId,
        //            QuestionId = model.QuestionId ?? 0,
        //            AiConfigId = question?.AiConfigId ?? 0,
        //            UserId = user1.UserId,
        //        });
        //        var userChatHistoryModel = (List<AiChatApiDto>)userChatHistory.Result;
        //        var messages = userChatHistoryModel.SelectMany(x => new List<MessageHelliGptDto>() {
        //            new MessageHelliGptDto(){
        //                Role = "user",
        //                Content= new List<ContentHelliGptDto>()
        //                {
        //                    new TextContentHelliGptDto()
        //                    {
        //                        Type = "text",
        //                        Text = x.UserMessage
        //                    }
        //                }
        //            },
        //            new MessageHelliGptDto(){
        //                Role = "assistant",
        //                Content= new List<ContentHelliGptDto>()
        //                {
        //                    new TextContentHelliGptDto()
        //                    {
        //                        Type = "text",
        //                        Text = x.AiResponse ?? ""
        //                    }
        //                }
        //            },
        //        }).ToList();
        //        string message = "";
        //        if (messages.Count == 0)
        //        {
        //            message = config.FirstMessage.FormatPrompt(promptDictionary);
        //            messages.Add(new MessageHelliGptDto()
        //            {
        //                Role = "user",
        //                Content = new List<ContentHelliGptDto>()
        //                {
        //                    new TextContentHelliGptDto()
        //                    {
        //                        Type = "text",
        //                        Text = message
        //                    }
        //                }
        //            });
        //        }
        //        else
        //        {
        //            message = model.Message;
        //            messages.Add(new MessageHelliGptDto()
        //            {
        //                Role = "user",
        //                Content = new List<ContentHelliGptDto>()
        //                {
        //                    new TextContentHelliGptDto()
        //                    {
        //                        Type = "text",
        //                        Text = message
        //                    }
        //                }
        //            });
        //        }
        //        var askHelliGpt = new AskHelliGptDtoV2()
        //        {
        //            ai = config.AiModel.Parent.Title,
        //            model = config.AiModel.Title,
        //            prompt = config.Prompt,
        //            messages = messages,
        //            n = config.N,
        //            temperature = config.Temperature,
        //            max_tokens = config.MaxTokens,
        //            top_p = config.TopP,
        //            presence_penalty = config.PresencePenalty,
        //            frequency_penalty = config.FrequencyPenalty,
        //            stop = config.Stop
        //        };
        //        var platform = config.AiPlatform;
        //        if (platform == null)
        //            return ResponseManager.DataError(SystemMessages.AiPlatformNotFound);
        //        var url = platform.ApiEndpoint;
        //        var client = new ApiClient(url, "post");
        //        client.Body = askHelliGpt;
        //        client.Timeout = TimeSpan.FromSeconds(180);
        //        var resApi = await client.Send<ServiceMessage>();
        //        if (resApi.StatusCode != HttpStatusCode.OK)
        //        {
        //            await log.Add((resApi.ErrorMessage ?? "") + $"PartId:{model.PartId}&QuestionId:{model.QuestionId}&UserId:{user1.UserId}", $"StatusCode:{resApi.StatusCode.ToString()}&UserMsg:{model.Message}", "AskFromBookAi");
        //        }
        //        var resultAi = resApi.Data?.ErrorId >= 0
        //            ? JsonConvert.DeserializeObject<AnswerHelliGptDto>(resApi.Data.Result.ToString())
        //            : null;
        //        var res = await mediator.Send(new AskFromAiRequest()
        //        {
        //            Model = new UpsertUserAiChatLogDto()
        //            {
        //                PartId = model.PartId,
        //                BookId = bookPart.BookId,
        //                UserId = user1.UserId,
        //                QuestionId = model.QuestionId,
        //                UserMessage = message,
        //                AiCouldResponse = (resApi.StatusCode == HttpStatusCode.OK && resApi.Data?.ErrorId >= 0),
        //                AiResponse = resApi.Data?.ErrorId >= 0 ? string.Join(',', resultAi?.response ?? new string[0]) : resApi.Data?.ErrorTitle,
        //                SummarizationCost = resultAi?.Cost?.Summarization_cost ?? 0,
        //                RequestCost = resultAi?.Cost?.Request_cost ?? 0,
        //                EmbeddingCost = resultAi?.Cost?.Embedding_cost ?? 0,
        //                DateTime = DateTime.UtcNow.AddHours(3.5),
        //                User = user1.Users,
        //                AiConfigId = config.Id,
        //            }
        //        });
        //        return res;
        //    }
        //    catch (Exception e)
        //    {
        //        await log.Add(e.Message, e.InnerException?.Message ?? "", "User Ask From Ai Chat Api In Api UserLikeOnBookPart");
        //        return ResponseManager.ServerError();
        //    }
        //}

        //[Route("GetBookQuestions")]
        //[HttpGet]
        //public async Task<ActionResult<ServiceMessage>> BookQuestions(int partId)
        //{
        //    try
        //    {
        //        var token = webTools.GetToken();
        //        if (string.IsNullOrEmpty(token))
        //            return ResponseManager.DataError(SystemMessages.TokenError);

        //        var user1 = await user.GetByToken(token);
        //        if (user1 == null || user1.Id == 0)
        //            return ResponseManager.SessionExpire();

        //        var userHasAiAccess = await user.Any(x => x.UserId == user1.UserId && x.HasAccessToAiChat);
        //        if (!userHasAiAccess)
        //            return ResponseManager.DataError(SystemMessages.UserAiAccessDenied);

        //        var bookPart = await part.GetPartById(partId);
        //        if (bookPart == null)
        //            return ResponseManager.DataError(SystemMessages.BookPartNotFound);

        //        var userHasAccessToBook = await userBooks.CheckUserHas(user1.UserId, bookPart.BookId);
        //        if (!userHasAccessToBook)
        //            return ResponseManager.DataError(SystemMessages.HasNoPermissionForBook);

        //        var questions = await bookQuestion.GetAll(x => x.PartId == partId && x.AiConfigId != null && !x.IsDelete);
        //        var res = mapper.Map<List<BookQueestionApiDto>>(questions);
        //        return ResponseManager.FillObject(res);
        //    }
        //    catch (Exception e)
        //    {
        //        await log.Add(e.Message, e.InnerException?.Message ?? "", "Get Book Questions In Api AiChat");
        //        return ResponseManager.ServerError();
        //    }
        //}
    }
}
