//using ClosedXML.Excel;
//using ExcelDataReader;
//using Live_Book.Application.Constants;
//using Live_Book.Application.Contracts.Persistence.Dapper;
//using Live_Book.Application.Contracts.Persistence.EfCore;
//using Live_Book.Application.DTOs;
//using Live_Book.Application.Features.AdvisorProfile.Request.Queries;
//using Live_Book.Classes;
//using Live_Book.Domain;
//using Live_Book.Domain.Enum;
//using Live_Book.Models;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using PersianAssistant.Extensions;

//namespace Live_Book.Areas.AdminArea.Controllers
//{
//	[ApiExplorerSettings(IgnoreApi = true)]
//    [PermissionChecker]
//    [Area("AdminArea")]
//    [Route("Admin/User")]
//    public class UserController : Controller
//    {
//        private readonly IUser _user;
//        private readonly IReadBookLog _readBookLog;
//        private readonly IGrades _grades;
//        private readonly IAdminActionDp _adminActionDp;
//        private readonly IUserBooks _userBooks;
//        private readonly AdminHelper _adminHelper;
//        private readonly IRequestLogin _requestLogin;
//        private readonly IErrorLog _errorLog;
//        private static int _addedRows = 0;
//        private static string _message = "";
//        private static bool _success = false;
//        private static string _reportExcelFileName = "";
//        private readonly IBook _book;
//        private readonly ITag _userTag;
//        private readonly IUserTags _userUserTags;
//        private readonly IProjectRepository _profileProject;
//        private readonly ISupervisorRepository _profileSupervisor;
//		private readonly IAdvisorRepository _profileAdvisor;
//        private readonly IMediator _mediator;

//        public AdminLoggedIn? Admin { get; set; }
//        public List<int> ProjectIds { get; set; }
//		public UserController(IUser user, IReadBookLog readBookLog, IGrades grades, 
//            IAdminActionDp adminActionDp, IUserBooks userBooks, AdminHelper adminHelper, 
//            IRequestLogin requestLogin, IErrorLog errorLog, IBook book, ITag userTag,
//            IUserTags userUserTags, IProjectRepository profileProject, IAdvisorRepository profileAdvisor,
//            ISupervisorRepository profileSupervisor, IMediator mediator)
//        {
//            _user = user;
//            _readBookLog = readBookLog;
//            _grades = grades;
//            _adminActionDp = adminActionDp;
//            _userBooks = userBooks;
//            _adminHelper = adminHelper;
//            _requestLogin = requestLogin;
//            _errorLog = errorLog;
//            _book = book;
//            _userTag = userTag;
//            _userUserTags = userUserTags;
//            _profileProject = profileProject;
//			_profileAdvisor = profileAdvisor;
//            _profileSupervisor = profileSupervisor;
//            _mediator = mediator;
//        }

//        [HttpPost]
//        [Route("Delete")]
//        public async Task<IActionResult> Delete(int id = 0)
//        {
//            var checkAdminResult = await CheckAdmin();
//            if (checkAdminResult != null)
//                return checkAdminResult;
//            try
//            {
//                //await _userBooks.RemoveRange(id);
//                var user = await _user.FirstOrDefault(x => x.UserId == id);
//                if(ProjectIds?.Count > 0 && !ProjectIds.Contains(user.ProjectId.Value))
//                    return Json(new
//                    {
//                        errorId = 1,
//                        errorTitle = UserImportExcelReason.InCompatibleProject,
//                    });
//                var userBooks = await _userBooks.GetAllBookId(id);
//                var userTags = await _userUserTags.GetAllUserTagId(id);
//                var adminId = _adminHelper.GetNameIdentifier();
//                var dataPosted = DataConvertor.PostUserToJson(user, userBooks, userTags, user.ProjectId);
//                await _adminActionDp.Add(new AdminAction()
//                {
//                    AdminId = adminId,
//                    TypeId = 1,
//                    Page = PersianPageTitle.User,
//                    Action = PersianActionPage.Delete,
//                    ObjectId = id,
//                    Data = dataPosted,
//                    DateTime = DateTime.UtcNow.AddHours(3.5),
//                    PreviousData = ""
//                });
//                await _user.Delete(user);
//                return Json(new
//                {
//                    errorId = 0,
//                });
//            }
//            catch (Exception e)
//            {
//                var message = e.Message ?? "";
//                var innerMessage = e?.InnerException?.Message ?? "";
//                string action = "Delete User";
//                await _errorLog.Add(message, innerMessage, action);
//                return Json(new
//                {
//                    errorId = -1,
//                    errorTitle = "خطایی رخ داده است لطفا با پشتیبانی تماس بگیرید."
//                });
//            }
//        }

//        [Route("Delete/DeleteUsersWithExcel")]
//        public async Task<IActionResult> DeleteUsersWithExcel()
//        {
//            var checkAdminResult = await CheckAdmin();
//            if (checkAdminResult != null)
//                return checkAdminResult;
//            _reportExcelFileName = "";
//            return View();
//        }

//        [HttpPost]
//        [Route("Delete/DeleteUsersWithExcel")]
//        public async Task<IActionResult> DeleteUsersWithExcel(IFormFile excel)
//        {
//            var checkAdminResult = await CheckAdmin();
//            if (checkAdminResult != null)
//                return checkAdminResult;
//            _addedRows = 0;
//            _message = "";
//            _reportExcelFileName = "";
//            try
//            {
//                if (excel == null)
//                {
//                    return Json(new
//                    {
//                        row = 0,
//                        message = "لطفا اکسل را وارد کنید",
//                        success = false,
//                        reportExcelFileName = "",
//                    });
//                }
//                var mainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "excel");
//                var filePath = Path.Combine(mainPath, "import", excel.FileName ?? "");
//                await using (Stream stream = new FileStream(filePath, FileMode.Create))
//                {
//                    await excel.CopyToAsync(stream);
//                }
//                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
//                var userHasProblem = new List<ImportExcelUserReport>();
//                await using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
//                {
//                    using (var reader = ExcelReaderFactory.CreateReader(stream))
//                    {
//                        var header = 1;
//                        var needBreak = false;
//                        do
//                        {
//                            if (needBreak)
//                            {
//                                break;
//                            }
//                            while (reader.Read()) //Each ROW
//                            {
//                                if (header == 1)
//                                {
//                                    header = 0;
//                                    continue;
//                                }

//                                if (reader.GetValue(0) == null && reader.GetValue(1) == null)
//                                {
//                                    break;
//                                }
//                                var helliCode = reader.GetValue(1)?.ToString() ?? "";

//                                var mobileNumber = reader.GetValue(0)?.ToString() ?? "";
//                                if (mobileNumber.Length != 10)
//                                {
//                                    userHasProblem.Add(new ImportExcelUserReport()
//                                    {
//                                        HelliCode = helliCode,
//                                        Reason = UserImportExcelReason.MobileNumberInvalid,
//                                        MobileNumber = mobileNumber,
//                                    });
//                                    continue;
//                                }
//                                mobileNumber = "0" + mobileNumber;

//                                int userCountWithThisData = 0;
//                                Domain.User user;
//                                _addedRows++;
//                                if (!string.IsNullOrEmpty(helliCode) && !string.IsNullOrEmpty(mobileNumber))
//                                {
//                                    userCountWithThisData = await _user.GetCount(u => !u.IsDelete && u.HelliCode == helliCode && u.MobileNumber == mobileNumber);
//                                    if (userCountWithThisData == 0)
//                                    {
//                                        userHasProblem.Add(new ImportExcelUserReport()
//                                        {
//                                            HelliCode = helliCode,
//                                            Reason = UserImportExcelReason.UserNoExist,
//                                            MobileNumber = mobileNumber,
//                                        });
//                                        continue;
//                                    }
//                                    if (userCountWithThisData > 1)
//                                    {
//                                        userHasProblem.Add(new ImportExcelUserReport()
//                                        {
//                                            HelliCode = helliCode,
//                                            Reason = UserImportExcelReason.ManyUserWithThisData,
//                                            MobileNumber = mobileNumber,
//                                        });
//                                        continue;
//                                    }
//                                    user = await _user.FirstOrDefault(x => !x.IsDelete && x.HelliCode == helliCode && x.MobileNumber == mobileNumber);
//                                    await _user.Delete(user);
//                                }
//                                else if (!string.IsNullOrEmpty(helliCode) && string.IsNullOrEmpty(mobileNumber))
//                                {
//                                    userCountWithThisData = await _user.GetCount(u => !u.IsDelete && u.HelliCode == helliCode);
//                                    if (userCountWithThisData == 0)
//                                    {
//                                        userHasProblem.Add(new ImportExcelUserReport()
//                                        {
//                                            HelliCode = helliCode,
//                                            Reason = UserImportExcelReason.UserNoExist,
//                                        });
//                                        continue;
//                                    }
//                                    if (userCountWithThisData > 1)
//                                    {
//                                        userHasProblem.Add(new ImportExcelUserReport()
//                                        {
//                                            HelliCode = helliCode,
//                                            Reason = UserImportExcelReason.ManyUserWithThisHelliCode,
//                                        });
//                                        continue;
//                                    }
//                                    user = await _user.FirstOrDefault(u => !u.IsDelete && u.HelliCode == helliCode);
//                                    if (ProjectIds?.Count > 0 && !ProjectIds.Contains(user.ProjectId.Value))
//                                    {
//                                        userHasProblem.Add(new ImportExcelUserReport()
//                                        {
//                                            HelliCode = helliCode,
//                                            Reason = UserImportExcelReason.InCompatibleProject,
//                                        });
//                                        continue;
//                                    }
                                        
//                                    await _user.Delete(user);
//                                }
//                                else if (string.IsNullOrEmpty(helliCode) && !string.IsNullOrEmpty(mobileNumber))
//                                {
//                                    userCountWithThisData = await _user.GetCount(u => !u.IsDelete && u.MobileNumber == mobileNumber);
//                                    if (userCountWithThisData == 0)
//                                    {
//                                        userHasProblem.Add(new ImportExcelUserReport()
//                                        {
//                                            MobileNumber = mobileNumber,
//                                            Reason = UserImportExcelReason.UserNoExist,
//                                        });
//                                        continue;
//                                    }
//                                    if (userCountWithThisData > 1)
//                                    {
//                                        userHasProblem.Add(new ImportExcelUserReport()
//                                        {
//                                            MobileNumber = mobileNumber,
//                                            Reason = UserImportExcelReason.MobileNumberExist,
//                                        });
//                                        continue;
//                                    }
//                                    user = await _user.FirstOrDefault(u => !u.IsDelete && u.MobileNumber == mobileNumber);
//                                    await _user.Delete(user);
//                                }
//                            }
//                        }
//                        while (reader.NextResult()); //Move to NEXT SHEET
//                    }
//                }
//                //if (System.IO.File.Exists(filePath))
//                //{
//                //    System.IO.File.Delete(filePath);
//                //}
//                var adminId = _adminHelper.GetNameIdentifier();
//                await _adminActionDp.Add(new AdminAction()
//                {
//                    AdminId = adminId,
//                    TypeId = 1,
//                    Page = PersianPageTitle.User,
//                    Action = PersianActionPage.ImportDeleteUserExcel,
//                    ObjectId = 0,
//                    Data = "",
//                    DateTime = DateTime.UtcNow.AddHours(3.5),
//                    PreviousData = ""
//                });
//                if (userHasProblem.Count == 0)
//                {
//                    _message = "عملیات موفق";
//                    _success = true;
//                }
//                else
//                {
//                    using (var workbook = new XLWorkbook())
//                    {
//                        var currentRow = 1;
//                        workbook.RightToLeft = true;
//                        string title = "ReportDeleteUserExcel" + DateTime.UtcNow.AddHours(3.5).ToPersianDate()?.Replace("/", "_");
//                        var worksheet = workbook.Worksheets.Add(title);
//                        worksheet.ColumnWidth = 20;
//                        worksheet.Cell(currentRow, 1).Value = "شماره همراه";
//                        worksheet.Cell(currentRow, 2).Value = "حلی کد";
//                        worksheet.Cell(currentRow, 3).Value = "دلیل";
//                        foreach (var item in userHasProblem)
//                        {
//                            currentRow++;
//                            worksheet.Cell(currentRow, 1).Value = item.MobileNumber ?? "-";
//                            worksheet.Cell(currentRow, 2).Value = item.HelliCode ?? "-";
//                            worksheet.Cell(currentRow, 3).Value = item.Reason;
//                        }
//                        using (var stream = new MemoryStream())
//                        {
//                            workbook.SaveAs(stream);
//                            var content = stream.ToArray();
//                            string fileName = Guid.NewGuid() + "_" + DateTime.UtcNow.AddHours(3.5).ToPersianDate().Replace("/", "_") + ".xlsx";
//                            string fullPath = Path.Combine(mainPath, "export", fileName);
//                            await using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
//                            {
//                                await fs.WriteAsync(content, 0, content.Length);
//                            }
//                            _reportExcelFileName = fileName;
//                            _success = false;
//                            _message = "عملیات با خطا مواجه شد برای دریافت اطلاعات بیشتر اکسل گزارش را دانلود کنید";
//                        }
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                var message = e.Message ?? "";
//                var innerMessage = e?.InnerException?.Message ?? "";
//                string action = "Delete users with excel";
//                await _errorLog.Add(message, innerMessage, action);
//                _message = $"داده های خط {_addedRows} چک شود";
//                _success = false;
//            }
//            return Json(new
//            {
//                result = ""
//            });
//        }

//        [Route("Delete/DeleteUsersExcelStatus")]
//        public async Task<IActionResult> DeleteUsersExcelStatus()
//        {
//            return Json(new
//            {
//                row = _addedRows,
//                message = _message,
//                success = _success,
//                reportExcelFileName = _reportExcelFileName,
//            });
//        }

//        [Route("Index/RequestLoginPaging/{type}/{userId}/{pageId}")]
//        public async Task<IActionResult> RequestLoginPagingInFullDataUser(int type = 1, int userId = 0, int pageId = 1)
//        {
//            try
//            {
//                var user = await _user.FirstOrDefault(x => x.UserId == userId);
//                if (user == null) return NotFound();
//                int take = PagingValue.Take5;
//                // type 1 برای پیج بندی درخواست های ورود کاربر 
//                // type 2 برای پیج بندی کتاب های مطالعه شده کاربر
//                var model = new FullInfoUserPaging();
//                if (type == 1)
//                {
//                    var requestLogin = await _requestLogin.GetAll(x => x.UserId == userId, Application.Tools.Utility.CalcSkip(pageId, take), take, 2);
//                    model.RequestLogins = requestLogin;
//                }

//                if (type == 2)
//                {
//                    var readLogs = await _readBookLog.GetAllLog(x => x.UserId == userId, Application.Tools.Utility.CalcSkip(pageId, take), take);
//                    model.BookReadLogs = readLogs;
//                }
//                return PartialView("FullInfoUserDataPaging", model);
//            }
//            catch (Exception e)
//            {
//                var ee = e.Message;
//                throw;
//            }
//        }

//        [Route("Add/AddUsersWithExce")]
//        public async Task<IActionResult> AddUsersWithExcel()
//        {
//            var checkAdminResult = await CheckAdmin();
//            if (checkAdminResult != null)
//                return checkAdminResult;
//            _reportExcelFileName = "";
//            await FillAddUserWithExcelDropDown();
//            return View();
//        }

//        [HttpPost]
//        [Route("Add/AddUsersWithExce")]
//        public async Task<IActionResult> AddUsersWithExcel(AddExcelUserViewModel InputExcel, bool DeleteLastUserBooks = false,
//            bool DeleteLastUserTags = false, bool updateMobileNumber = false)
//        {
//            var checkAdminResult = await CheckAdmin();
//            if (checkAdminResult != null)
//                return checkAdminResult;
//            _addedRows = 0;
//            _message = "";
//            _reportExcelFileName = "";
//            try
//            {
//                if (InputExcel.Excel != null)
//                {
//                    var MainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "excel");
//                    var filePath = Path.Combine(MainPath, "import", (InputExcel.Excel.FileName));
//                    using (Stream stream = new FileStream(filePath, FileMode.Create))
//                    {
//                        await InputExcel.Excel.CopyToAsync(stream);
//                    }
//                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
//                    var userHasProblem = new List<ImportExcelUserReport>();
//                    await using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
//                    {
//                        using (var reader = ExcelReaderFactory.CreateReader(stream))
//                        {
//                            int header = 1, updateCounter = 0, counter = 0;
//                            var needBreak = false;
//                            do
//                            {
//                                if (needBreak)
//                                {
//                                    break;
//                                }
//                                while (reader.Read()) //Each ROW
//                                {
//                                    if (header == 1)
//                                    {
//                                        header = 0;
//                                        continue;
//                                    }

//                                    if (reader.GetValue(0) == null && reader.GetValue(1) == null && reader.GetValue(2) == null && reader.GetValue(3) == null)
//                                    {
//                                        break;
//                                    }

//                                    var firstName = "";
//                                    if (reader?.GetValue(1) != null)
//                                    {
//                                        firstName = reader.GetValue(1).ToString();
//                                    }

//                                    var lastName = "";
//                                    if (reader?.GetValue(2) != null)
//                                    {
//                                        lastName = reader.GetValue(2).ToString();
//                                    }

//                                    var helliCode = "";
//                                    if (reader?.GetValue(0) != null)
//                                    {
//                                        helliCode = reader.GetValue(0).ToString();
//                                        if (string.IsNullOrEmpty(helliCode) || helliCode.Length != 11)
//                                        {
//                                            userHasProblem.Add(new ImportExcelUserReport()
//                                            {
//                                                HelliCode = helliCode,
//                                                Reason = string.IsNullOrEmpty(helliCode) ? UserImportExcelReason.HelliCodeEmpty : UserImportExcelReason.HelliCodeLength,
//                                                MobileNumber = "-",
//                                                FirstName = firstName ?? "-",
//                                                LastName = lastName ?? "-"
//                                            });
//                                            continue;
//                                        }
//                                    }

//                                    var mobileNumber = "";
//                                    if (reader?.GetValue(3) != null)
//                                    {
//                                        string number = reader.GetValue(3).ToString() ?? "";
//                                        if (string.IsNullOrEmpty(number) || number.Length != 10 || !number.StartsWith("9"))
//                                        {
//                                            userHasProblem.Add(new ImportExcelUserReport()
//                                            {
//                                                HelliCode = helliCode,
//                                                Reason = string.IsNullOrEmpty(number) ? UserImportExcelReason.MobileNumberEmpty : UserImportExcelReason.MobileNumberInvalid,
//                                                MobileNumber = number,
//                                                FirstName = firstName,
//                                                LastName = lastName
//                                            });
//                                            continue;
//                                        }
//                                        mobileNumber = "0" + number;
//                                    }

//                                    var usersWithThisHelliCode = await _user.GetCount(x => x.HelliCode == helliCode && !x.IsDelete);
//                                    if (usersWithThisHelliCode > 1)
//                                    {
//                                        userHasProblem.Add(new ImportExcelUserReport()
//                                        {
//                                            HelliCode = helliCode,
//                                            Reason = UserImportExcelReason.ManyUserWithThisHelliCode,
//                                            MobileNumber = mobileNumber,
//                                            FirstName = firstName ?? "-",
//                                            LastName = lastName ?? "-"
//                                        });
//                                        continue;
//                                    }

//                                    // هر شماره می تواند به چند حلی کد متصل باشد
//                                    var user = await _user.FirstOrDefault(x => x.HelliCode == helliCode && !x.IsDelete);
//                                    if (user == null)
//                                    {
//                                        user = new User
//                                        {
//                                            MobileNumber = mobileNumber,
//                                            FirstName = firstName,
//                                            LastName = lastName,
//                                            Name = firstName + " " + lastName,
//                                            HelliCode = helliCode,
//                                            Grade = InputExcel.GradeId,
//                                            //UserTagId = InputExcel.UserTagId,
//                                            ProjectId = ProjectIds?.Count == 1 ? ProjectIds[0] : InputExcel.ProjectId,
//                                            Gender = 3,
//                                            ActiveCode = 5.GenerateCode(),
//                                        };
//                                        var userId = await _user.Add(user);
//                                        if (userId == 0 || userId == null)
//                                        {
//                                            userHasProblem.Add(new ImportExcelUserReport()
//                                            {
//                                                HelliCode = helliCode,
//                                                Reason = UserImportExcelReason.ErrorInAddUser,
//                                                MobileNumber = mobileNumber,
//                                                FirstName = firstName ?? "-",
//                                                LastName = lastName ?? "-"
//                                            });
//                                            continue;
//                                        }
//                                        if (InputExcel.BookId?.Count > 0)
//                                        {
//                                            await _userBooks.AddRange(userId, InputExcel.BookId);
//                                        }
//                                        if (InputExcel.UserTagId?.Count > 0 || InputExcel.ParentUserTagId?.Count > 0)
//                                        {
//											List<int> UserTags = new List<int>();
//											if (InputExcel.UserTagId != null) { UserTags.AddRange(InputExcel.UserTagId); }
//											if (InputExcel.ParentUserTagId != null) { UserTags.AddRange(InputExcel.ParentUserTagId); }
//											await _userUserTags.AddRange(user.UserId, UserTags);
//										}
//                                    }
//                                    else
//                                    {
//                                        // project manager can't change other projects student
//                                        if (ProjectIds?.Count > 0 && !ProjectIds.Contains(user.ProjectId ?? 0))
//                                        {
//                                            userHasProblem.Add(new ImportExcelUserReport()
//                                            {
//                                                HelliCode = helliCode,
//                                                Reason = UserImportExcelReason.InCompatibleProject,
//                                                MobileNumber = mobileNumber,
//                                                FirstName = firstName ?? "-",
//                                                LastName = lastName ?? "-"
//                                            });
//                                            continue;
//                                        }
//                                        user.FirstName = firstName;
//                                        user.LastName = lastName;
//                                        user.Name = firstName + " " + lastName;
//                                        user.Grade = InputExcel.GradeId;
//                                        user.UpdateDateTime = DateTime.UtcNow.AddHours(3.5);
//                                        if(ProjectIds == null || ProjectIds.Count == 0)
//                                            user.ProjectId = InputExcel.ProjectId;
//                                        await _user.Upsert(user);
//                                        if (DeleteLastUserBooks)
//                                        {
//                                            await _userBooks.RemoveRange(user.UserId);
//                                        }
//                                        if (InputExcel.BookId?.Count > 0)
//                                        {
//                                            await _userBooks.AddRange(user.UserId, InputExcel.BookId);
//                                        }
//                                        if (DeleteLastUserTags)
//                                        {
//                                            await _userUserTags.RemoveRange(user.UserId);
//                                        }
//                                        if (InputExcel.UserTagId?.Count > 0 || InputExcel.ParentUserTagId?.Count > 0)
//										{
//											List<int> UserTags = new List<int>(); 
//                                            if (InputExcel.UserTagId != null) { UserTags.AddRange(InputExcel.UserTagId); }
//											if (InputExcel.ParentUserTagId != null) { UserTags.AddRange(InputExcel.ParentUserTagId); }
//											await _userUserTags.AddRange(user.UserId, UserTags);
//										}
//									}
//                                    _addedRows++;
//                                }
//                            }
//                            while (reader.NextResult()); //Move to NEXT SHEET
//                        }
//                        //if (System.IO.File.Exists(filePath))
//                        //{
//                        //    System.IO.File.Delete(filePath);
//                        //}
//                    }
//                    var adminId = _adminHelper.GetNameIdentifier();
//                    await _adminActionDp.Add(new AdminAction()
//                    {
//                        AdminId = adminId,
//                        TypeId = 1,
//                        Page = PersianPageTitle.User,
//                        Action = PersianActionPage.ImportNewUserExcel,
//                        ObjectId = 0,
//                        Data = $"آپدیت شماره تلفن کاربران : {updateMobileNumber}" + $"حذف کتاب های قبلی کاربر : {DeleteLastUserBooks}",
//                        DateTime = DateTime.UtcNow.AddHours(3.5),
//                        PreviousData = ""
//                    });
//                    if (userHasProblem.Count == 0)
//                    {
//                        _message = "عملیات موفق";
//                        _success = true;
//                    }
//                    else
//                    {
//                        using (var workbook = new XLWorkbook())
//                        {
//                            var currentRow = 1;
//                            workbook.RightToLeft = true;
//                            string title = "ReportUserImport" + DateTime.UtcNow.AddHours(3.5).ToPersianDate()?.Replace("/", "_");
//                            var worksheet = workbook.Worksheets.Add(title);
//                            worksheet.ColumnWidth = 20;
//                            worksheet.Cell(currentRow, 1).Value = "حلی کد";
//                            worksheet.Cell(currentRow, 2).Value = "نام";
//                            worksheet.Cell(currentRow, 3).Value = "نام خانوادگی";
//                            worksheet.Cell(currentRow, 4).Value = "شماره همراه";
//                            worksheet.Cell(currentRow, 5).Value = "دلیل";
//                            foreach (var item in userHasProblem)
//                            {
//                                currentRow++;
//                                worksheet.Cell(currentRow, 1).Value = item.HelliCode;
//                                worksheet.Cell(currentRow, 2).Value = item.FirstName;
//                                worksheet.Cell(currentRow, 3).Value = item.LastName;
//                                worksheet.Cell(currentRow, 4).Value = item.MobileNumber;
//                                worksheet.Cell(currentRow, 5).Value = item.Reason;
//                            }
//                            using (var stream = new MemoryStream())
//                            {
//                                workbook.SaveAs(stream);
//                                var content = stream.ToArray();
//                                string fileName = Guid.NewGuid() + "_" + DateTime.UtcNow.AddHours(3.5).ToPersianDate().Replace("/", "_") + ".xlsx";
//                                string fullPath = Path.Combine(MainPath, "export", fileName);
//                                await using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
//                                {
//                                    await fs.WriteAsync(content, 0, content.Length);
//                                }
//                                _reportExcelFileName = fileName;
//                                _success = false;
//                                _message = "عملیات با خطا مواجه شد برای دریافت اطلاعات بیشتر اکسل گزارش را دانلود کنید";
//                            }
//                        }
//                    }
//                }
//                else
//                {
//                    return Json(new
//                    {
//                        row = 0,
//                        message = "لطفا اکسل و تنظیمات را وارد کنید",
//                        success = false,
//                        reportExcelFileName = "",
//                    });
//                }
//            }
//            catch (Exception e)
//            {
//                var message = e.Message ?? "";
//                var innerMessage = e?.InnerException?.Message ?? "";
//                string action = "ImportUserExcel";
//                await _errorLog.Add(message, innerMessage, action);
//                _message = $"داده های خط {_addedRows} چک شود";
//                _success = false;
//            }
//            return Json(new
//            {
//                result = ""
//            });
//        }

//		[Route("Add/AddAdvisorToUsersWithExce")]
//        public async Task<IActionResult> AddAdvisorToUsersWithExcel()
//        {
//            var checkAdminResult = await CheckAdmin();
//            if (checkAdminResult != null)
//                return checkAdminResult;
//            _reportExcelFileName = "";
//			return View();
//		}
//        [HttpPost]
//        [Route("Add/AddAdvisorToUsersWithExce")]
//        public async Task<IActionResult> AddAdvisorToUsersWithExcel(UpdateUserExcelViewModel InputExcel)
//        {
//            var checkAdminResult = await CheckAdmin();
//            if (checkAdminResult != null)
//                return checkAdminResult;
//            _addedRows = 0;
//            _message = "";
//            _reportExcelFileName = "";
//            try
//            {
//                if (InputExcel.Excel != null)
//                {
//                    var MainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "excel");
//                    var filePath = Path.Combine(MainPath, "import", (InputExcel.Excel.FileName));
//                    using (Stream stream = new FileStream(filePath, FileMode.Create))
//                    {
//                        await InputExcel.Excel.CopyToAsync(stream);
//                    }
//                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
//                    var userHasProblem = new List<IdTitle>();
//                    await using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
//                    {
//                        using (var reader = ExcelReaderFactory.CreateReader(stream))
//                        {
//                            int header = 1, updateCounter = 0, counter = 0;
//                            var needBreak = false;
//                            Dictionary<string, AdvisorProfile> advisors = new Dictionary<string, AdvisorProfile>();
//                            do
//                            {
//                                if (needBreak)
//                                {
//                                    break;
//                                }
//                                while (reader.Read()) //Each ROW
//                                {
//                                    if (header == 1)
//                                    {
//                                        header = 0;
//                                        continue;
//                                    }

//                                    if (reader.GetValue(0) == null && reader.GetValue(1) == null)
//                                    {
//                                        break;
//                                    }
//                                    _addedRows++;
//                                    var userHelliCode = "";
//                                    if (reader?.GetValue(0) != null)
//                                    {
//                                        userHelliCode = reader.GetValue(0).ToString();
//                                        if (string.IsNullOrEmpty(userHelliCode) || userHelliCode.Length != 11)
//                                        {
//                                            userHasProblem.Add(new IdTitle()
//                                            {
//                                                Id = _addedRows,
//                                                Title = string.IsNullOrEmpty(userHelliCode) ? UserImportExcelReason.HelliCodeEmpty : UserImportExcelReason.HelliCodeEmpty,
//                                            });
//                                            continue;
//                                        }
//                                    }

//                                    var advisorHelliCode = "";
//                                    if (reader?.GetValue(1) != null)
//                                    {

//                                        advisorHelliCode = reader.GetValue(1).ToString();
//                                        if (string.IsNullOrEmpty(advisorHelliCode) || advisorHelliCode.Length != 11)
//                                        {
//                                            userHasProblem.Add(new IdTitle()
//                                            {
//                                                Id = _addedRows,
//                                                Title = string.IsNullOrEmpty(advisorHelliCode) ? UserImportExcelReason.advisorHelliCodeEmpty : UserImportExcelReason.advisorHelliCodeLength,
//                                            });
//                                            continue;
//                                        }
//                                    }
//                                    var user = await _user.FirstOrDefault(x => x.HelliCode == userHelliCode && !x.IsDelete);
//                                    if (user == null)
//                                    {
//                                        userHasProblem.Add(new IdTitle()
//                                        {
//                                            Id = _addedRows,
//                                            Title = "کاربری با این حلی کد یافت نشد",
//                                        });
//                                        continue;
//                                    }
//                                    AdvisorProfile advisor = null;
//                                    if (!advisors.ContainsKey(advisorHelliCode)) {
//                                        var advisorUser = await _user.FirstOrDefault(x => x.HelliCode == advisorHelliCode && !x.IsDelete);
//                                        if (advisorUser == null)
//                                        {
//                                            userHasProblem.Add(new IdTitle()
//                                            {
//                                                Id = _addedRows,
//                                                Title = "مشاوری با این حلی کد یافت نشد",
//                                            });
//                                            continue;
//                                        }
//                                        if (advisorUser.AdminId == null || advisorUser.AdminId == 0)
//                                        {
//                                            userHasProblem.Add(new IdTitle()
//                                            {
//                                                Id = _addedRows,
//                                                Title = "حلی کد وارد شده، مشاور نیست",
//                                            });
//                                            continue;
//                                        }
//                                        advisor = await _profileAdvisor.FirstOrDefault(x => x.AdminId == advisorUser.AdminId);
//                                        if (advisor != null)
//                                            advisors.Add(advisorHelliCode, advisor);
//                                    }
//                                    else
//                                    {
//                                        advisor = advisors[advisorHelliCode];
//                                    }
//                                    if (advisor == null)
//                                    {
//                                        userHasProblem.Add(new IdTitle()
//                                        {
//                                            Id = _addedRows,
//                                            Title = "مشاوری با این حلی کد یافت نشد",
//                                        });
//                                        continue;
//                                    }
//                                    if(ProjectIds?.Count > 0 && (!ProjectIds.Contains(user.ProjectId ?? 0) || (!ProjectIds.Contains(advisor.ProjectId))))
//                                    {
//                                        userHasProblem.Add(new IdTitle()
//                                        {
//                                            Id = _addedRows,
//                                            Title = UserImportExcelReason.InCompatibleProject,
//                                        });
//                                        continue;
//                                    }
//                                    if (user.ProjectId != advisor.ProjectId)
//                                    {
//                                        userHasProblem.Add(new IdTitle()
//                                        {
//                                            Id = _addedRows,
//                                            Title = "پروژه کاربر با مشاور همخوانی ندارد",
//                                        });
//                                        continue; 
//                                    }
//                                    user.AdvisorId = advisor.Id;
//                                    await _user.Upsert(user);
//                                }
//                            }
//                            while (reader.NextResult()); //Move to NEXT SHEET
//                        }
//                    }
//                    var adminId = _adminHelper.GetNameIdentifier();
//                    await _adminActionDp.Add(new AdminAction()
//                    {
//                        AdminId = adminId,
//                        TypeId = 1,
//                        Page = PersianPageTitle.User,
//                        Action = PersianActionPage.ImportAddAdvisorToUsersExcel,
//                        ObjectId = 0,
//                        Data = "",
//                        DateTime = DateTime.UtcNow.AddHours(3.5),
//                        PreviousData = ""
//                    });
//                    if (userHasProblem.Count == 0)
//                    {
//                        _message = "عملیات موفق";
//                        _success = true;
//                    }
//                    else
//                    {
//                        using (var workbook = new XLWorkbook())
//                        {
//                            var currentRow = 1;
//                            workbook.RightToLeft = true;
//                            string title = "ReportUserImport" + DateTime.UtcNow.AddHours(3.5).ToPersianDate()?.Replace("/", "_");
//                            var worksheet = workbook.Worksheets.Add(title);
//                            worksheet.ColumnWidth = 20;
//                            worksheet.Cell(currentRow, 1).Value = "ردیف";
//                            worksheet.Cell(currentRow, 2).Value = "مشکل";
//                            foreach (var item in userHasProblem)
//                            {
//                                currentRow++;
//                                worksheet.Cell(currentRow, 1).Value = item.Id + 1;
//                                worksheet.Cell(currentRow, 2).Value = item.Title;
//                            }
//                            using (var stream = new MemoryStream())
//                            {
//                                workbook.SaveAs(stream);
//                                var content = stream.ToArray();
//                                string fileName = Guid.NewGuid() + "_" + DateTime.UtcNow.AddHours(3.5).ToPersianDate().Replace("/", "_") + ".xlsx";
//                                string fullPath = Path.Combine(MainPath, "export", fileName);
//                                await using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
//                                {
//                                    await fs.WriteAsync(content, 0, content.Length);
//                                }
//                                _reportExcelFileName = fileName;
//                                _success = false;
//                                _message = "عملیات با خطا مواجه شد برای دریافت اطلاعات بیشتر اکسل گزارش را دانلود کنید";
//                            }
//                        }
//                    }
//                }
//                else
//                {
//                    return Json(new
//                    {
//                        row = 0,
//                        message = "لطفا اکسل و تنظیمات را وارد کنید",
//                        success = false,
//                        reportExcelFileName = "",
//                    });
//                }
//            }
//            catch (Exception e)
//            {
//                var message = e.Message ?? "";
//                var innerMessage = e?.InnerException?.Message ?? "";
//                string action = "ImportUserExcel";
//                await _errorLog.Add(message, innerMessage, action);
//                _message = $"داده های خط {_addedRows} چک شود";
//                _success = false;
//            }
//            return Json(new
//            {
//                result = ""
//            });
//        }
//        [Route("Index/ExcelStatus")]
//        public async Task<IActionResult> ExcelStatus()
//        {
//            return Json(new
//            {
//                row = _addedRows,
//                message = _message,
//                success = _success,
//                reportExcelFileName = _reportExcelFileName,

//            });

//        }

//		#region Utility
//		private async Task FillAddUserWithExcelDropDown()
//        {
//            ViewData["ProjectId"] = ProjectIds?.Count == 1 ? ProjectIds[0] : null;
//            ViewData["Grades"] = new SelectList(await _grades.GetAll(), "GradeId", "GradeTitle");
//            ViewData["ProjectProfile"] = ProjectIds == null || ProjectIds.Count == 0 ? new SelectList(await _profileProject.GetAll(), "Id", "Title") : null;
//            ViewData["ParentUserTags"] = new SelectList(await _userTag.GetAll(x =>
//                x.ParentId == null &&
//                (ProjectIds == null || ProjectIds.Count > 0
//            ?  x.ProjectId == null || ProjectIds.Contains(x.ProjectId ?? 0)
//				: x.ProjectId == null)),
//            "Id", "Title");
//            SelectList? books = null;
//            if(ProjectIds?.Count > 0)
//            {
//				books = new SelectList(await _book.GetAllBooks(x => !x.IsDelete), "Id", "Name");
//            }

//            ViewData["Books"] = books;
//            ViewData["ParentAdvisors"] = ProjectIds?.Count > 0 ?
//                new SelectList(await _mediator.Send(new GetAdvisorsByRequest() { ProjectIds = ProjectIds, ForChild = false }), "Id", "Title")
//                : null;
//        }
//        public async Task<IActionResult?> CheckAdmin()
//        {
//            Admin = await _adminHelper.GetAdminLoggedIn();
//            if (Admin == null)
//                return Redirect(Urls.SignOut);
//            if(Admin.AdminType == AdminTypeIdEnum.GeneralAdmin || Admin.AdminType == AdminTypeIdEnum.Supervisor || Admin.AdminType == AdminTypeIdEnum.ProjectManager)
//            {
//                if (Admin.HasAccessToAllProjects)
//                {
//					ProjectIds = null;
//				}
//                else{
//                    ProjectIds = Admin.ProjectIds;
//                }
//            }
//            else // for now advisor and parent advisor don't have permission to add users
//                return Redirect(Urls.NoPermission);
//            return null;
//        }
//        #endregion
//    }
//}
