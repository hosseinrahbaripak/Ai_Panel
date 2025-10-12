//using System.Net;
//using System.Security.Claims;
//using ClosedXML.Excel;
//using DocumentFormat.OpenXml.Spreadsheet;
//using GoogleRecaptcha.Models;
//using Ai_Panel.Application.Contracts.Persistence.EfCore;
//using Ai_Panel.Application.DTOs;
//using Ai_Panel.Application.Tools;
//using Ai_Panel.Domain;
//using Ai_Panel.Domain.Enum;
//using Ai_Panel.Models;
//using Microsoft.CodeAnalysis;
//using Newtonsoft.Json;
//using PersianAssistant.Extensions;


//namespace Ai_Panel.Classes
//{
//    public static class ExcelHelper
//    {
//        public static string UserExcelCreator(string title = "", List<User> model = null)
//        {
//            using (var workbook = new XLWorkbook())
//            {
//                var currentRow = 1;
//                workbook.RightToLeft = true;
//                string fileName = title + DateTime.UtcNow.AddHours(3.5).ToPersianDate()?.Replace("/", "_");
//                var worksheet = workbook.Worksheets.Add(title);
//                worksheet.Cell(currentRow, 1).Value = "نام";
//                worksheet.Cell(currentRow, 2).Value = "پایه";
//                worksheet.Cell(currentRow, 3).Value = "حلی کد";
//                worksheet.Cell(currentRow, 4).Value = "شماره همراه";
//                worksheet.Cell(currentRow, 5).Value = "تاریخ ثبت نام";
//                foreach (var item in model)
//                {
//                    currentRow++;
//                    worksheet.Cell(currentRow, 1).Value = item.Name;
//                    worksheet.Cell(currentRow, 2).Value = EnumVal.GradeType(item.Grade != null ? item.Grade.Value : 0);
//                    worksheet.Cell(currentRow, 3).Value = item.HelliCode;
//                    worksheet.Cell(currentRow, 4).Value = item.MobileNumber;
//                    worksheet.Cell(currentRow, 5).Value = item.DateTime.ToPersianDate();
//                }
//                using (var stream = new MemoryStream())
//                {
//                    workbook.SaveAs(stream);
//                    var content = stream.ToArray();
//                    var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "excel", "user", fileName);
//                    File.WriteAllBytes(path, content);
//                    return path;
//                }
//            }
//        }
//    }
//    public class AdminHelper
//    {
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly IAdminManage _adminManage;
//        private readonly IRoleInPages _roleInPages;
//        private readonly IAdvisorRepository _advisorRepository;
//        private readonly ISupervisorRepository _supervisorRepository;
//        public AdminHelper(IHttpContextAccessor httpContextAccessor, IAdminManage adminManage, IRoleInPages roleInPages,
//            IAdvisorRepository advisorRepository, ISupervisorRepository supervisorRepository)
//        {
//            _httpContextAccessor = httpContextAccessor;
//            _adminManage = adminManage;
//            _roleInPages = roleInPages;
//            _advisorRepository = advisorRepository;
//            _supervisorRepository = supervisorRepository;
//        }
//        public async Task<AdminLoggedIn?> GetAdminLoggedIn(bool includePages = false)
//        {
//            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
//            int adminLoginId = int.Parse(claimsIdentity?.FindFirst(CustomClaimTypes.AdminLoginId)?.Value ?? "0");
//            var admin = await _adminManage.FirstOrDefault(x => x.LoginID == adminLoginId && !x.IsDelete);
//            if (admin == null)
//                return null;
//            List<Domain.Pages> pages = new List<Domain.Pages>();
//            if (includePages)
//                pages = _roleInPages.GetAll(x => x.RoleId == admin.RoleId, null, "Pages").Result.Select(x => x.Pages).ToList();
//            var adminLoggedIn = new AdminLoggedIn()
//            {
//                UserName = admin.UserName,
//                UserId = admin.UserId,
//                Email = admin.Email,
//                Pages = string.Join(",", pages.Select(x => x.PageAddress)),
//                ProjectIds = new List<int>(),
//                AdminType = (AdminTypeIdEnum)admin.Role.AdminTypeId.Value,
//                IsSuperAdmin = admin.IsSuperAdmin,
//                RoleId = admin.RoleId,
//                AdminProfileId = int.Parse(claimsIdentity.FindFirst(CustomClaimTypes.AdminProfileId)?.Value),
//                AdminLoginId = int.Parse(claimsIdentity.FindFirst(CustomClaimTypes.AdminLoginId)?.Value),
//            };
//            switch (adminLoggedIn.AdminType)
//            {
//                case AdminTypeIdEnum.GeneralAdmin:
//					adminLoggedIn.HasAccessToAllProjects = true;
//					break;
//                case AdminTypeIdEnum.ProjectManager: 
//                    adminLoggedIn.ProjectIds.Add(adminLoggedIn.AdminProfileId);
//                    break;
//                case AdminTypeIdEnum.ParentAdvisor:
//                case AdminTypeIdEnum.Advisor:
//					var advisor = await _advisorRepository.Get(adminLoggedIn.AdminProfileId);
//					adminLoggedIn.ProjectIds.Add(advisor.ProjectId);
//					break;
//				case AdminTypeIdEnum.Supervisor:
//					var supervisor = await _supervisorRepository.FirstOrDefault(x=> x.Id == adminLoggedIn.AdminProfileId, null, "Projects");
//					if (supervisor.Projects?.Count > 0)
//					{
//						adminLoggedIn.ProjectIds.AddRange(supervisor.Projects.Where(x=> x.ProjectId.HasValue).Select(x => x.ProjectId.Value));
//						adminLoggedIn.HasAccessToAllProjects = false;
//					}
//					else
//					{
//						adminLoggedIn.HasAccessToAllProjects = true;
//					}
//					break;
//			}
//            return adminLoggedIn;
//        }
//        public int GetNameIdentifier()
//        {
//            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
//            int adminLoginId = int.Parse(claimsIdentity?.FindFirst(CustomClaimTypes.AdminLoginId)?.Value ?? "0");
//            return adminLoginId;
//        }
//    }
//    public static class DataConvertor
//    {
//        public static string PostUserToJson(User user, List<int>? booksId, List<int>? userTagsId, int? projectId)
//        {
//            var model = new AdminPostUserModel()
//            {
//                UserId = user.UserId,
//                Grade = user.Grade,
//                HelliCode = user.HelliCode,
//                MobileNumber = user.MobileNumber,
//                FirstName = user.FirstName,
//                LastName = user.LastName,
//                BooksId = booksId,
//                ProjectId = projectId,
//                Gender = user.Gender
//            };
//            return JsonConvert.SerializeObject(model);
//        }

//        public static AdminPostUserModel DeserializePostedUser(string data = "")
//        {
//            if (string.IsNullOrEmpty(data)) { return null; }
//            var model = JsonConvert.DeserializeObject<AdminPostUserModel>(data);
//            return model;
//        }

//        public static string PostAdminToJson(AdminLogin admin)
//        {
//            var model = new AdminPostAdminModel()
//            {
//                RoleId = admin.RoleId,
//                UserName = admin.UserName,
//                Email = admin.Email,
//                AdminId = admin.LoginID,
//                Password = admin.Password
//            };
//            return JsonConvert.SerializeObject(model);
//        }

//        public static AdminPostAdminModel DeserializePostedAdmin(string data = "")
//        {
//            if (string.IsNullOrEmpty(data)) { return null; }
//            var model = JsonConvert.DeserializeObject<AdminPostAdminModel>(data);
//            return model;
//        }
//		public static string PostAdvisorToJson(AdvisorProfile advisor)
//		{
//			var model = new AdvisorPostAdminModel()
//			{
//				RoleId = advisor.Admin.RoleId,
//				UserName = advisor.Admin.UserName,
//				Email = advisor.Admin.Email,
//				AdminId = advisor.Admin.LoginID,
//				Password = advisor.Admin.Password,
//                AdminUserId = advisor.Admin.UserId,
//                ProjectId = advisor.ProjectId,
//				AdvisorId = advisor.Id,
//                Title = advisor.Title,
//			};
//			return JsonConvert.SerializeObject(model);
//		}
//		public static AdminPostAdminModel DeserializePostedAdvisor(string data = "")
//		{
//			if (string.IsNullOrEmpty(data)) { return null; }
//			var model = JsonConvert.DeserializeObject<AdvisorPostAdminModel>(data);
//			return model;
//		}
//	}
//    public static class ExtensionMethod
//    {
//        public static int CalcDiscountPercent(this int price, int percent)
//        {
//            if (percent == 0)
//            {
//                return price;
//            }
//            int orgPercent = 100 - percent;
//            double orgPrice = Convert.ToDouble(orgPercent) / 100;
//            double result = price * orgPrice;
//            return Convert.ToInt32(result);
//        }

//    }
//    public static class ReCaptcha
//    {
//        public static async Task<bool> GoogleRecaptcha(IFormCollection form)
//        {
//            string urlToPost = "https://www.google.com/recaptcha/api/siteverify";
//            string secretKey = "6Lff7V8mAAAAAKpke3xAs0mRyvNGyFipxp9dndDn"; // change this
//            string gRecaptchaResponse = form["g-recaptcha-response"];

//            var postData = "secret=" + secretKey + "&response=" + gRecaptchaResponse;

//            // send post data
//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlToPost);
//            request.Method = "POST";
//            request.ContentLength = postData.Length;
//            request.ContentType = "application/x-www-form-urlencoded";

//            await using (var streamWriter = new StreamWriter(request.GetRequestStream()))
//            {
//                await streamWriter.WriteAsync(postData);
//            }

//            // receive the response now
//            string result = string.Empty;
//            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
//            {
//                using (var reader = new StreamReader(response.GetResponseStream()))
//                {
//                    result = await reader.ReadToEndAsync();
//                }
//            }

//            // validate the response from Google reCaptcha
//            var captchaResponse = JsonConvert.DeserializeObject<reCaptchaResponse>(result);
//            if (captchaResponse == null) { return false; }
//            if (!captchaResponse.Success)
//            {
//                return false;
//            }

//            return true;
//        }
//    }
//    public static class Convertor
//    {
//        public static object ConvertList(List<object> value, Type type)
//        {
//            var containedType = type.GenericTypeArguments.First();
//            return value.Select(item => Convert.ChangeType(item, containedType)).ToList();
//        }
//    }
//}
