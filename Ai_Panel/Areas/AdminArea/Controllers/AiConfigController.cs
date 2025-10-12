//using System.Net;
//using Ai_Panel.Application.Constants;
//using Ai_Panel.Application.Contracts.Persistence.EfCore;
//using Ai_Panel.Application.DTOs.AiChat;
//using Ai_Panel.Application.DTOs.AiConfig;
//using Ai_Panel.Application.Features.AiConfig.Request.Queries;
//using Ai_Panel.Application.Features.TestAiConfig.Request.Command;
//using Ai_Panel.Application.Tools;
//using Ai_Panel.Classes;
//using Ai_Panel.Domain;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Newtonsoft.Json;
//using PersianAssistant.Extensions;
//using PersianAssistant.Models;

//namespace Ai_Panel.Areas.AdminArea.Controllers
//{
//    [ApiExplorerSettings(IgnoreApi = true)]
//    [PermissionChecker]
//    [Area("AdminArea")]
//    [Route("Admin/AiConfig")]
//    public class AiConfigController(
//        IMediator mediator,
//        IErrorLog log,
//		IBookGroup bookGroupRepo,
//        IBook bookRepo,
//		IBookPart partRepo,
//		IBookQuestionRepository bookQuestionRepo,
//        ITestAiConfigRepository testAiConfigRepo,
//		IConfiguration configuration,
//		IAiPlatformRepository aiPlatformRepo,
//        AdminHelper adminHelper) : Controller
//	{
//        [Route("Add/AskAi")]
//        [HttpPost]
//        public async Task<ActionResult<ServiceMessage>> AskAi([FromForm] TestAiConfigDto model)
//        {
//			try
//            {
//                var admin = await adminHelper.GetAdminLoggedIn();
//                if(admin == null)
//                    return ResponseManager.DataError("شما دسترسی ندارید");
//                var content = "";
//                var promptDictionary = new Dictionary<string, string>();
//                var part = await partRepo.GetPartById(model.PartId ?? 0);
//                promptDictionary["Book"] = part?.Book?.Name ?? "";
//                promptDictionary["Part"] = part?.PartName ?? "";
//				promptDictionary["User"] = admin.UserName;
//				if (model.QuestionId != null)
//				{
//					var question = await bookQuestionRepo.FirstOrDefault(x => x.Id == model.QuestionId);
//                    if (question == null)
//						return ResponseManager.DataError(SystemMessages.QuestionNotFound);
//					if (question.PartId != model.PartId)
//						return ResponseManager.DataError(SystemMessages.QuestionInBookPartNotFound);
//					promptDictionary["Question"] = question.Question;
//					promptDictionary["Answer"] = question.Answer;
//					promptDictionary["Content"] = question.Content ?? "";
//				}
//				else
//                {
//                    content = model.BookContent;
//				}
//				 var chatHistory= await testAiConfigRepo.GetAll(x=>
//				    x.AdminId == admin.AdminLoginId &&
//					x.AiModelId == model.AiModelId && 
//                    x.DateTime > model.TestDateTimeBegging
//				);
//                var messages = chatHistory.SelectMany(x => new List<MessageHelliGptDto>() {
//					new MessageHelliGptDto(){
//						Role = "user",
//						Content= new List<ContentHelliGptDto>()
//                        {
//                            new TextContentHelliGptDto()
//                            {
//                                Type = "text",
//                                Text = x.Message
//                            }
//                        }
//					},
//					new MessageHelliGptDto(){
//						Role = "assistant",
//						Content= new List<ContentHelliGptDto>()
//						{
//							new TextContentHelliGptDto()
//							{
//								Type = "text",
//								Text = x.AiResponse ?? ""
//							}
//						}
//					},
//				}).ToList();
//                if(messages.Count == 0)
//                {
//					messages.Add(new MessageHelliGptDto()
//					{
//						Role = "user",
//						Content = new List<ContentHelliGptDto>()
//					    {
//						    new TextContentHelliGptDto()
//						    {
//							    Type = "text",
//							    Text = model.FirstMessage.FormatPrompt(promptDictionary)
//							},
//                            //new ImageContentHelliGptDto()
//                            //{
//                            //    Type = "image",
//                            //    ImageUrl = ""
//                            //}
//					    }
//					});
//                }
//                else
//                {
//					messages.Add(new MessageHelliGptDto()
//					{
//						Role = "user",
//						Content = new List<ContentHelliGptDto>()
//					    {
//						    new TextContentHelliGptDto()
//						    {
//							    Type = "text",
//							    Text = model.Message
//						    },
//                            //new ImageContentHelliGptDto()
//                            //{
//                            //    Type = "image",
//                            //    ImageUrl = ""
//                            //}
//					    }
//					});
//				}
//                var askHelliGpt = new AskHelliGptDtoV2()
//                {
//                    ai = model.AiStr,
//                    model = model.AiModelStr,
//                    prompt = model.Prompt,
//                    messages = messages,
//                    n = model.N,
//                    temperature = model.Temperature,
//                    max_tokens = model.MaxTokens,
//                    top_p = model.TopP,
//                    presence_penalty = model.PresencePenalty,
//                    frequency_penalty = model.FrequencyPenalty,
//                    stop = model.Stop
//				};
//                var client = new HttpClient();
//                client.Timeout = TimeSpan.FromSeconds(180);
//				var platform = await aiPlatformRepo.Get(model.AiPlatformId);
//				if (platform == null)
//					return ResponseManager.DataError(SystemMessages.AiPlatformNotFound);
//                var url = platform.ApiEndpoint;
//				var resApi = await client.PostAsJsonAsync(url, askHelliGpt);
//				var resGpt = new ServiceMessage();
//                if (resApi.StatusCode == HttpStatusCode.OK)
//                {
//                    var resGptStr = await resApi.Content.ReadAsStringAsync();
//                    resGpt = JsonConvert.DeserializeObject<ServiceMessage>(resGptStr);
//					if (resGpt == null || resGpt.ErrorId < 0)
//					{
//						await log.Add((resGpt?.ErrorTitle ?? "") + $"PartId:{model.PartId}&QuestionId:{model.QuestionId}&AdminId:{admin.AdminLoginId}", $"ErrorId:{resGpt?.ErrorId}&UserMsg:{model.Message}", "TestAiCnfig");
//						return ResponseManager.ServerError();
//					}
//                }
//                else
//                {
//                    await log.Add((resApi.ReasonPhrase ?? "") + $"PartId:{model.PartId}&QuestionId:{model.QuestionId}&AdminId:{admin.AdminLoginId}", $"StatusCode:{resApi.StatusCode.ToString()}&UserMsg:{model.Message}", "TestAiCnfig");
//					return ResponseManager.ServerError();
//				}
//                AnswerHelliGptDto resultAi = resGpt.ErrorId >= 0 ? resGpt.Result.ToObject<AnswerHelliGptDto>() : null;
//                    if (resultAi != null)
//                    {
//                        model.AiResponse = string.Join(" , ", resultAi.response);
//                        model.SummarizationCost = resultAi.Cost?.Summarization_cost ?? 0;
//                        model.RequestCost = resultAi.Cost?.Request_cost ?? 0;
//                        model.EmbeddingCost = resultAi.Cost?.Embedding_cost ?? 0;
//                    }
//                    model.AdminId = admin.AdminLoginId;
//                var res = await mediator.Send(new TestAiConfigRequest()
//                {
//                    Model = model
//                });
//                return res;
//            }
//            catch (Exception e)
//            {
//                await log.Add(e.Message, e.InnerException?.Message ?? "",
//                    "User Ask From Ai Chat Api In Api UserLikeOnBookPart");
//                return ResponseManager.ServerError();
//            }
//        }
//		[Route("Add/ToBookQuestions", Name = "AddAiConfigToBookQuestions")]
//		public async Task<ActionResult> AddAiConfigToBookQuestions(int id)
//        {
//			var aiConfigDto = await mediator.Send(new GetAiConfigRequest()
//			{
//				Id = id
//			});
//            if(aiConfigDto == null)
//            {
//                ViewBag.Error = SystemMessages.AiConfigNotFound;
//				return Redirect(Urls.AiConfig + ActionPage.Index);
//            }
//			var bookGroups = await bookGroupRepo.GetAllGroupsAsync(bc => bc.Book != null && bc.Book.Any(b => !b.IsDelete));
//			var books = await bookRepo.GetAllBooks(x => !x.IsDelete);
//			var bookParts = await partRepo.GetBookParts(x => !x.IsDelete && x.AiContentId == null);
//			var bookQuestions = await bookQuestionRepo.GetAll(x => !x.IsDelete, q => q.OrderBy(x => x.IsMultipleChoice).ThenBy(x => x.Index), "Part");
//			ViewBag.BookGroup = new SelectList(bookGroups, "GroupId", "GroupTitle");
//			ViewBag.Books = JsonConvert.SerializeObject(books.Select(x => new
//			{
//				Id = x.Id,
//				GroupId = x.GroupId,
//				Title = x.Name
//			}));
//			ViewBag.BookParts = JsonConvert.SerializeObject(bookParts.Select(x => new
//			{
//				Id = x.PartId,
//				BookId = x.BookId,
//				Title = x.PartName
//			}));
//			ViewBag.BookQuestions = JsonConvert.SerializeObject(bookQuestions.Select(x => new
//			{
//				Id = x.Id,
//				PartId = x.PartId,
//                PartName = x.Part.PartName,
//				Index = x.Index,
//				IsMultipleChoice = x.IsMultipleChoice
//			}));
//            ViewBag.AiConfig = aiConfigDto;
//			return View(new AddAiConfigToQuestionDto() { AiConfigId = id});
//		}
//		[Route("Add/ToBookQuestions")]
//		[HttpPost]
//		public async Task<IActionResult> AddToBookQuestion(AddAiConfigToQuestionDto model)
//		{
//            List<BookQuestion> questions;
//            if(model.PartIds == null || model.PartIds.Count == 0)
//            {
//               questions = await bookQuestionRepo.GetAll(x=> model.BookIds.Contains(x.BookId));
//            }
//            else
//            {
//				if (model.QuestionIds == null || model.QuestionIds.Count == 0)
//				{
//					questions = await bookQuestionRepo.GetAll(x => model.PartIds.Contains(x.PartId));
//				}
//				else
//				{
//					questions = await bookQuestionRepo.GetAll(x => model.QuestionIds.Contains(x.Id));
//				}
//			}
//   //         if(questions == null || questions.Count == 0)
//   //         {
//			//	ViewBag.Error = SystemMessages.QuestionNotFound;
//			//	return Redirect(Urls.AiConfig + ActionPage.Add + "/ToBookQuestions?id=" + model.AiConfigId);
//			//} 
//            foreach (var question in questions)
//            {
//                question.AiConfigId = model.AiConfigId;
//                await bookQuestionRepo.Update(question);
//            }
//			return Redirect(Urls.AiConfig + ActionPage.Index);
//		}
//	}
//}