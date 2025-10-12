//using Live_Book.Application.Contracts.Persistence.EfCore;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;

//namespace Live_Book.Controllers.UserApp;
//[Route("UserApp")]
//public class UserAppController(IUser userRepository,  book, IMediator mediator) : Controller
//{
//    public int UserId { get; set; }

//    [Route("GetReadLog")]
//    [HttpGet]
//    public async Task<IActionResult> GetReadLog(string token)
//    {
//        if (string.IsNullOrEmpty(token))
//        {
//            return Redirect("/Error404");
//        }
//        var session = await userRepository.GetByToken(token);
//        if (session == null)
//        {
//            return Redirect("/Error404");
//        }
//        UserId = session.UserId;
//        return View(UserId);
//    }
//}
