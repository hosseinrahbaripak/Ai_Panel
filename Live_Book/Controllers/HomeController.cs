//using Ai_Panel.Application.Contracts.Persistence.EfCore;
//using Ai_Panel.Domain;
//using Ai_Panel.Domain.Enum;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Diagnostics;
//using Microsoft.AspNetCore.Mvc;

//namespace Ai_Panel.Controllers
//{
//    [ApiExplorerSettings(IgnoreApi = true)]
//    public class HomeController : Controller
//    {
//        private readonly IBook _Book;
//        private readonly IUser _user;
//        private readonly IReadBookLog _readBookLog;
//        private readonly IFrequentlyQuestion _frequentlyQuestion;
//        private readonly IAppVersion _appVersion;
//        private readonly IGrades _grades;
//        private readonly IErrorLog _errorLog;
//        public HomeController(IBook book, IUser user, IReadBookLog readBookLog, IFrequentlyQuestion frequentlyQuestion, IAppVersion appVersion, IGrades grade, IErrorLog errorLog)
//        {
//            _Book = book;
//            _user = user;
//            _readBookLog = readBookLog;
//            _frequentlyQuestion = frequentlyQuestion;
//            _appVersion = appVersion;
//            _grades = grade;
//            _errorLog = errorLog;
//        }

//        public IActionResult Index()
//        {
//            if (User.Identity.IsAuthenticated)
//            {
//                return Redirect("/Admin/Home/Index");
//            }
//            else
//            {
//                return RedirectToPage("/Admin/Login");
//            }
//        }

//        [Route("NoPermission")]
//        public IActionResult NoPermission()
//        {
//            return View();
//        }

//        [Authorize]
//        [Route("Home/ShowFile")]
//        public IActionResult ShowFile(string fileName)
//        {
//            string archive = Path.Combine(Directory.GetCurrentDirectory(),
//                "wwwroot", "files", fileName);
//            if (!System.IO.File.Exists(archive))
//            {
//                return NotFound();
//            }

//            return PhysicalFile(archive, "application/epub+zip", fileName);
//        }

//        [Authorize]
//        [Route("ReadBook/{ReadToken}/{bookId}")]
//        public async Task<IActionResult> ReadBook(string ReadToken, int bookId)
//        {
//            var user = await _readBookLog.GetUserByReadToken(ReadToken);
//            if (user == null)
//            {
//                return NotFound();
//            }
//            //var timeend = user.TokenDateCreate.AddMinutes(30);
//            //if (timeend >= DateTime.UtcNow.AddHours(3.5))
//            //{
//            //    await _readBookLog.EditBookReadLog(user.UserId, bookId);
//            //    /*await _Book.GetBookNameById(bookId);*/
//            //    ViewBag.BookName = "dent-green-eagle.epub";
//            //    return View("Ebook");
//            //}
//            //else
//            //{
//            //    return NotFound();

//            //}
//            return NoPermission();
//        }

//        //[Route("Books")]
//        //public async Task<IActionResult> Books(int groupId = 0)
//        //{
//        //    var model = new BookShopViewModel();
//        //    model.BookCategories = await _Book.GetAllExistBookCategories();
//        //    ViewBag.GroupId = groupId;
//        //    if (groupId == 0)
//        //    {
//        //        var books = await _Book.GetAllBooks();
//        //        model.Book = books;
//        //    }
//        //    else
//        //    {
//        //        var books = await _Book.GetAllBooks(x => x.GroupId == groupId);
//        //        model.Book = books;
//        //    }
//        //    return View(model);
//        //    //ViewBag.GradeId = new SelectList(await _grades.GetAllGrades(), "GradeId", "GradeTitle");
//        //}

//        [Route("learnapp")]
//        public IActionResult LearnApp()
//        {
//            return View();
//        }

//        public async Task<IActionResult> DownloadVersion(string versionType, AppSystemTypeEnum systemType)
//        {
//            var app = await _appVersion.LastOrDefault(x => x.IsDelete == false && x.VersionType == versionType && x.AppSystemType == systemType, x => x.OrderBy(o => o.Id));
//            if (app == null || await _appVersion.AnyAsync(x=> !x.IsDelete && x.Id > app.Id && x.AppSystemType == systemType))
//                return NotFound();
//            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "app", app.FileName);
//            if (!System.IO.File.Exists(filePath)) return NotFound();
//            var fileExtension = Path.GetExtension(filePath);
//            var fileName = "کتاب خوان علامه" + fileExtension;
//            var contentType = "application/octet-stream"; // Set the appropriate content type
//            return PhysicalFile(filePath, contentType, fileName);
//        }

//        [Authorize]
//        [Route("Books/Ebook")]
//        public IActionResult Ebook()
//        {
//            return View();
//        }

//        public IActionResult Error404()
//        {
//            return View();
//        }

//        [Route("faq")]
//        public async Task<IActionResult> Faq()
//        {
//            var res = await _frequentlyQuestion.GetAll();
//            if (res.Count == 0)
//            {
//                ViewBag.Error = "به زودی در این قسمت سوالات متداول قرار میگیرد.";
//            }
//            return View(res);
//        }

//        [Route("Download")]
//        public async Task<IActionResult> Download()
//        {
//            var model = new List<Tuple<AppSystemTypeEnum, AppVersions>>();
//            var lastAndroid = await _appVersion.LastOrDefault(x => x.IsDelete == false && x.AppSystemType == AppSystemTypeEnum.Android, x => x.OrderBy(o => o.Id));
//            if (lastAndroid != null)
//            {
//                model.Add(Tuple.Create(AppSystemTypeEnum.Android, lastAndroid));
//            }
//            var lastWindows = await _appVersion.LastOrDefault(x => x.IsDelete == false && x.AppSystemType == AppSystemTypeEnum.Windows, x => x.OrderBy(o => o.Id));
//            if (lastWindows != null)
//            {
//                model.Add(Tuple.Create(AppSystemTypeEnum.Windows, lastWindows));
//            }
//            var lastIos = await _appVersion.LastOrDefault(x => x.IsDelete == false && x.AppSystemType == AppSystemTypeEnum.Ios, x => x.OrderBy(o => o.Id));
//            if (lastIos != null)
//            {
//                model.Add(Tuple.Create(AppSystemTypeEnum.Ios, lastIos));
//            }
//            return View(model);
//        }

//        [Authorize]
//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public async Task<IActionResult> Error()
//        {
//            var token = Guid.NewGuid();
//            var exceptionHandlerPathFeature =
//                HttpContext.Features.Get<IExceptionHandlerPathFeature>();
//            if (exceptionHandlerPathFeature != null)
//            {
//                await _errorLog.Add(exceptionHandlerPathFeature?.Error?.Message ?? "",
//                    exceptionHandlerPathFeature?.Error?.InnerException?.ToString() ?? "", exceptionHandlerPathFeature?.Path ?? "");
//            }

//            ViewBag.Token = token;
//            return View();
//        }
//    }
//}