using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs;
using Ai_Panel.Domain;
using Ai_Panel.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Ai_Panel.Application.DTOs.User;
using Ai_Panel.Application.Tools;
using PersianAssistant.Extensions;
using ResponseManager = PersianAssistant.Extensions.ResponseManager;
using ServiceMessage = PersianAssistant.Models.ServiceMessage;

namespace Ai_Panel.Persistence.Repository.EfCore
{
    public class UserRepository:IUser
    {
        private readonly LiveBookContext _db;
        public UserRepository(LiveBookContext context)
        {
            _db = context;
        }
        public async Task<List<User>> GetAll(Expression<Func<User, bool>> @where = null, int skip = 0, int take = int.MaxValue)
        {
            if (where == null)
            {
                return await _db.Users.Where(x => x.IsDelete == false)
                    .OrderByDescending(u => u.DateTime).Skip(skip).Take(take).ToListAsync();
            }
            return await _db.Users.Where(x => x.IsDelete == false)
                .Where(where)
                .OrderByDescending(u => u.DateTime).Skip(skip).Take(take).ToListAsync();
        }

        //public async Task<List<User>> GetAllDeleted(Expression<Func<User, bool>> where = null, int skip = 0, int take = int.MaxValue)
        //{
        //    if (where == null)
        //    {
        //        return await _db.Users.IgnoreQueryFilters().Where(x => x.IsDelete == true).OrderByDescending(u => u.DateTime).Skip(skip).Take(take).ToListAsync();
        //    }
        //    return await _db.Users.IgnoreQueryFilters().Where(x => x.IsDelete == true).Where(x => x.IsDelete == false).Where(where)
        //        .OrderByDescending(u => u.DateTime).Skip(skip).Take(take).ToListAsync();
        //}
        //public async Task<int> GetCount(Expression<Func<User, bool>> @where = null)
        //{
        //    if (where == null)
        //    {
        //        return await _db.Users.Where(x => x.IsDelete == false).CountAsync();
        //    }
        //    return await _db.Users.Where(x => x.IsDelete == false).Where(where).CountAsync();
        //}
        //public async Task<int> GetCountDeleted(Expression<Func<User, bool>> where = null)
        //{
        //    if (where == null)
        //    {
        //        return await _db.Users.IgnoreQueryFilters().Where(x => x.IsDelete == true).CountAsync();
        //    }
        //    return await _db.Users.IgnoreQueryFilters().Where(x => x.IsDelete == true).Where(where).CountAsync();
        //}
        public async Task<User> FirstOrDefault(Expression<Func<User, bool>> @where = null, bool include = false)
        {
            if (where == null)
            {
                return null;
            }

            if (include)
            {
                var user = await _db.Users.Where(x => x.IsDelete == false)
                    .FirstOrDefaultAsync(where);
                return user;
            }
            else
            {
                var user = await _db.Users.Where(x => x.IsDelete == false).FirstOrDefaultAsync(where);
                return user;
            }
        }
        public async Task<bool> Any(Expression<Func<User, bool>> where = null)
        {
            return await _db.Users.AnyAsync(where);
        }
        //public async Task<int> Add(User user)
        //{
        //    try
        //    {
        //        user.DateTime = DateTime.UtcNow.AddHours(3.5);
        //        user.Status = 1;
        //        user.ActiveCode = 5.GenerateCode();
        //        await _db.Users.AddAsync(user);
        //        await _db.SaveChangesAsync();
        //        return user.UserId;
        //    }
        //    catch (Exception e)
        //    {
        //    }

        //    return 0;
        //}
        public async Task Upsert(User user)
        {
            try
            {
                if (user.UserId == 0)
                {
                    user.DateTime = DateTime.UtcNow.AddHours(3.5);
                    user.Status = 1;
                    user.ActiveCode = 5.GenerateCode();
                    await _db.Users.AddAsync(user);
                }
                else
                {
                    _db.Users.Update(user);
                }
            }
            catch (Exception e)
            {

            }
            await _db.SaveChangesAsync();
        }
        //public async Task Delete(int userId)
        //{
        //    var user = await FirstOrDefault(u => u.UserId == userId);
        //    if (user != null)
        //    {
        //        //_db.Users.Remove(model);
        //        //await _db.SaveChangesAsync();
        //        user.IsDelete = true;
        //        await Upsert(user);
        //    }
        //}
        //public async Task Delete(User user)
        //{
        //    //_db.Users.Remove(user);
        //    //await _db.SaveChangesAsync();
        //    user.IsDelete = true;
        //    await Upsert(user);
        //}
        //public async Task<string> CreateToken(int userId)
        //{
        //    var token = Guid.NewGuid().ToString();
        //    await _db.UserSessions.AddAsync(new UserSession()
        //    {
        //        DateTime = DateTime.UtcNow.AddHours(3.5),
        //        Token = token,
        //        UserId = userId,
        //    });
        //    await _db.SaveChangesAsync();
        //    return token;
        //}
        //public async Task<string> EditToken(int userId)
        //{
        //    var token = Guid.NewGuid().ToString();
        //    var res = await _db.UserSessions.Include(x => x.Users)
        //        .FirstOrDefaultAsync(u => u.Users.IsDelete == false && u.UserId == userId);
        //    res.Token = token;
        //    _db.UserSessions.Update(res);
        //    await _db.SaveChangesAsync();
        //    return token;
        //}
        //public async Task<int> GetTotalInstallationCount(Expression<Func<UserSession, bool>> where = null)
        //{
        //    var res = await _db.UserSessions.Where(where).Select(x => x.UserId).Distinct().CountAsync();
        //    return res;
        //}
        //public async Task<UserSession> GetByToken(string token)
        //{
        //    return await _db.UserSessions.Include(u => u.Users)
        //        .FirstOrDefaultAsync(u => u.Users.IsDelete == false && !u.IsLogout && u.Token == token);
        //}
        //public async Task<string> Login(string mobile)
        //{
        //    var user = await FirstOrDefault(u => u.MobileNumber == mobile) ?? new User()
        //    {
        //        MobileNumber = mobile,
        //    };
        //    user.ActiveCode = 5.GenerateCode();
        //    await Upsert(user);
        //    return user.ActiveCode;
        //}

        //public async Task<Tuple<string?, int?>> LoginV4(string mobile, string? helliCode, bool createUser = false)
        //{
        //    string activeCode = 5.GenerateCode();
        //    if (string.IsNullOrEmpty(helliCode))
        //    {
        //        var user = await _db.Users.FirstOrDefaultAsync(x => x.MobileNumber == mobile && x.HelliCode == mobile && !x.IsDelete);
        //        if (user == null)
        //        {
        //            var users = await _db.Users.Where(x => x.MobileNumber == mobile && !x.IsDelete).ToListAsync();
        //            if (users.Count == 0)
        //            {
        //                if (!createUser)
        //                    return new Tuple<string?, int?>(null, null);
        //                user = new User()
        //                {
        //                    MobileNumber = mobile,
        //                    HelliCode = mobile,
        //                    Gender = 3,
        //                    Status = 5,
        //                    //UserTagId = 6, // تگ کاربران عادی
        //                    ActiveCode = activeCode
        //                };
        //                await _db.AddAsync(user);
        //                await _db.SaveChangesAsync();
        //                return new Tuple<string?, int?>(activeCode, user.UserId);
        //            }
        //            if (users.Count == 1)
        //            {
        //                users.First().ActiveCode = activeCode;
        //                await Upsert(users.First());
        //                return new Tuple<string?, int?>(activeCode, users.First().UserId);
        //            }
        //            foreach (var userInLoop in users)
        //            {
        //                userInLoop.ActiveCode = activeCode;
        //                await Upsert(userInLoop);
        //            }
        //            return new Tuple<string?, int?>(activeCode, null);
        //        }
        //        else
        //        {
        //            user.ActiveCode = activeCode;
        //            await Upsert(user);
        //            return new Tuple<string?, int?>(activeCode, user.UserId);
        //        }
        //    }
        //    else
        //    {
        //        var user = await _db.Users.FirstOrDefaultAsync(x => x.MobileNumber == mobile && x.HelliCode == helliCode && !x.IsDelete);
        //        if (user == null)
        //        {
        //            if (!createUser)
        //                return new Tuple<string?, int?>(null, null);
        //            user = new User()
        //            {
        //                MobileNumber = mobile,
        //                HelliCode = helliCode,
        //                Gender = 3,
        //                Status = 5,
        //                //UserTagId = 6, // تگ کاربران عادی
        //                ActiveCode = activeCode
        //            };
        //            await _db.AddAsync(user);
        //            await _db.SaveChangesAsync();
        //            return new Tuple<string?, int?>(activeCode, user.UserId);
        //        }
        //        user.ActiveCode = activeCode;
        //        await Upsert(user);
        //        return new Tuple<string?, int?>(activeCode, user.UserId);
        //    }
        //}
        //public async Task<UserViewModel> Activation(string mobile, string activeCode)
        //{
        //    var user = new User();
        //    if (activeCode == "19530")
        //    {
        //        user = await FirstOrDefault(u => u.MobileNumber == mobile);
        //    }
        //    else
        //    {
        //        user = await FirstOrDefault(u => u.MobileNumber == mobile && u.ActiveCode == activeCode);
        //    }
        //    if (user == null)
        //    {
        //        return null;
        //    }
        //    var token = await CreateToken(user.UserId);
        //    return new UserViewModel()
        //    {
        //        MobileNumber = user.MobileNumber,
        //        DateTime = user.DateTime,
        //        Email = user.Email,
        //        FirstName = user.FirstName,
        //        Gender = user.Gender,
        //        LastName = user.LastName,
        //        NationalCode = user.NationalCode,
        //        Token = token,
        //        HasAccessToAiChat = user.HasAccessToAiChat
        //    };
        //}

        //public async Task<UserViewModel> UpdateProfile(string token, UserViewModel model)
        //{
        //    var userSession = await GetByToken(token);
        //    if (userSession == null)
        //    {
        //        return null;
        //    }

        //    var user = await FirstOrDefault(u => u.UserId == userSession.UserId);
        //    user.Email = model.Email;
        //    user.FirstName = model.FirstName;
        //    user.LastName = model.LastName;
        //    user.NationalCode = model.NationalCode;
        //    user.Gender = model.Gender;
        //    await Upsert(user);
        //    return new UserViewModel()
        //    {
        //        MobileNumber = user.MobileNumber,
        //        DateTime = user.DateTime,
        //        Email = user.Email,
        //        FirstName = user.FirstName,
        //        Gender = user.Gender,
        //        LastName = user.LastName,
        //        NationalCode = user.NationalCode,
        //        Token = token
        //    };
        //}
        //public async Task DeActiveSession(int sessionId)
        //{
        //    var res = await _db.UserSessions.FirstOrDefaultAsync(x => x.Id == sessionId && x.IsLogout == false);
        //    if (res != null)
        //    {
        //        res.IsLogout = true;
        //        res.DateLogout = DateTime.UtcNow.AddHours(3.5);
        //        _db.UserSessions.Update(res);
        //        await _db.SaveChangesAsync();
        //    }
        //}
        //public async Task MyQuery()
        ////List<string> HelliCodes)
        //{
        //int KomyteId = 1;
        //int BonyadId = 2;
        //int ShahrdaryId = 7;
        //int HekmatId = 6;
        //int MoassehId = 5;
        ////var projectProfiles = _db.ProjectProfiles.ToListAsync();
        //var KomyteUsers = await _db.Users.Where(
        //	x => x.UserTagId == 1 || x.UserTagId == 4 || x.UserTagId == 13 || x.UserTagId == 14 || x.UserTagId == 15)
        //	.ToListAsync();
        //var BonyadUsers = await _db.Users.Where(x => x.UserTagId == 17).ToListAsync();
        //var ShahrdaryUsers = await _db.Users.Where(x => x.UserTagId == 7).ToListAsync();
        //var HekmatUsers = await _db.Users.Where(x => x.UserTagId == 9).ToListAsync();
        //var MoassehUsers = await _db.Users.Where(x => x.UserTagId == 5 || x.UserTagId == 8 || x.UserTagId == 12 || x.UserTagId == 16).ToListAsync();
        //foreach (var KomyteUser in KomyteUsers)
        //{
        //	KomyteUser.ProjectId = KomyteId;
        //	//_db.Users.Update(KomyteUser);
        //}
        //foreach (var BonyadUser in BonyadUsers)
        //{
        //	BonyadUser.ProjectId = BonyadId;
        //	//_db.Users.Update(BonyadUser);
        //}
        //foreach (var ShahrdaryUser in ShahrdaryUsers)
        //{
        //	ShahrdaryUser.ProjectId = ShahrdaryId;
        //	//_db.Users.Update(ShahrdaryUser);
        //}
        //foreach (var HekmatUser in HekmatUsers)
        //{
        //	HekmatUser.ProjectId = HekmatId;
        //	//_db.Users.Update(HekmatUser);
        //}
        //foreach (var MoassehUser in MoassehUsers)
        //{
        //	MoassehUser.ProjectId = MoassehId;
        //	//_db.Users.Update(MoassehUser);
        //}

        //_db.SaveChanges();

        //var usersWithOutHelliCode = await _db.Users.Where(x => !x.IsDelete && (x.HelliCode == null || x.HelliCode == "")).ToListAsync();
        //int bug = 0;
        //int take = 5000;
        //var pages = (usersWithOutHelliCode.Count / take) + 1;
        //for (int i = 1; i <= pages; i++)
        //{
        //    int skip = Utility.CalcSkip(i, 5000);
        //    foreach (var user in usersWithOutHelliCode.Skip(skip).Take(take))
        //    {
        //        //var anyMobile = await _db.Users.AnyAsync(x => !x.IsDelete && x.UserId != user.UserId && (x.MobileNumber == user.MobileNumber) && (x.HelliCode == user.HelliCode));
        //        //if (anyMobile)
        //        //{
        //        //    bug++;
        //        //    continue;
        //        //}
        //        user.HelliCode = user.MobileNumber;
        //        _db.Update(user);
        //    }
        //    await _db.SaveChangesAsync();
        //}
        //var usersHasProblem = new List<Tuple<string, string>>();
        //foreach (var helliCode in HelliCodes)
        //{
        //    try
        //    {
        //        var user = await _db.Users.Where(x => x.HelliCode == helliCode && x.IsDelete == false).ToListAsync();
        //        if (user.Count() > 1)
        //        {
        //            usersHasProblem.Add(new Tuple<string, string>(helliCode, "many users exist with this helli code"));
        //            continue;
        //        }
        //        else
        //        {
        //            var userBook = await _db.UserBooks.FirstOrDefaultAsync(x => x.BookId == 122 && x.UserId == user.First().UserId);
        //            if (userBook != null)
        //            {
        //                _db.UserBooks.Remove(userBook);
        //                _db.SaveChanges();
        //            }
        //            UserBook book = new UserBook()
        //            {
        //                UserId = user.First().UserId,
        //                BookId = 2192
        //            };
        //            _db.UserBooks.Add(book);
        //            _db.SaveChanges();

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        usersHasProblem.Add(new Tuple<string, string>(helliCode, e.Message));
        //    }

        //}



        //try
        //{
        //    string numbers = "";
        //    for (int i = 3; i < 18; i++)
        //    {
        //        int skip = i * 2000;
        //        //var users = await _db.Users.Where(x => x.MobileNumber.StartsWith("0") == false).ToListAsync();
        //var users = await _db.Users.Where(x => x.MobileNumber.StartsWith("0") && x.MobileNumber.Length != 11).ToListAsync();
        //foreach (var user in users)
        //{
        //    user.IsDelete = true;
        //    await Upsert(user);
        //}
        //        if (users.Count > 0)
        //        {
        //            foreach (var user in users)
        //            {
        //                user.MobileNumber = user.MobileNumber.TrimStart('+');
        //                if (user.MobileNumber.Length > 10)
        //                {
        //                    numbers += user.MobileNumber + "-";
        //                    var notePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "User.txt");
        //                    if (File.Exists(notePath))
        //                    {
        //                        await File.WriteAllTextAsync(notePath, numbers);
        //                    }
        //                }
        //                else
        //                {
        //                    user.MobileNumber = "0" + user.MobileNumber;
        //                    await Upsert(user);
        //                }
        //            }
        //        }
        //        else
        //        {

        //        }
        //    }
        //}
        //catch (Exception e)
        //{
        //    var ee = e.Message;
        //}


        ////پیدا کردن کاربر های تکراری و حذف ان ها
        //var users = await _db.Users.Where(x => x.IsDelete == false).GroupBy(x => x.HelliCode).Where(x => x.Count() > 1).Select(x => x.Key).ToListAsync();
        //var notePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Users.txt");
        //if (!File.Exists(notePath))
        //{
        //    return;
        //}
        //var numbers = "";
        //foreach (var item in users)
        //{
        //    numbers += item + "\r";
        //    await File.WriteAllTextAsync(notePath, numbers);
        //}

        //foreach (var item in users)
        //{
        //    var user = await _db.Users.Where(x => x.MobileNumber == item).ToListAsync();
        //    //if (user.Count() == 2)
        //    //{
        //    //    if (user.First().HelliCode == user.Last().HelliCode)
        //    //    {
        //    //        if (user.Last().IsDelete == true)
        //    //        {

        //    //        }
        //    //        if (user.First().IsDelete == false)
        //    //        {

        //    //        }

        //    //        var logs = _db.BookReadLogs.Where(x => x.UserId == user.First().UserId);
        //    //        foreach (var log in logs)
        //    //        {
        //    //            log.UserId = user.Last().UserId;
        //    //            _db.Update(log);
        //    //            await _db.SaveChangesAsync();
        //    //        }
        //    //        var books = _db.UserBooks.Where(x => x.UserId == user.First().UserId).ToList();
        //    //        _db.UserBooks.RemoveRange(books);
        //    //        _db.Users.Remove(user.First());
        //    //        await _db.SaveChangesAsync();
        //    //    }
        //    //}
        //    //user.FirstOrDefault().IsDelete = true;
        //    //await Upsert(user.FirstOrDefault());
        //}


        // یافتن کاربر های کلا ویرایش شده
        //var res = await _db.AdminAction.Where(x => x.TypeId == 1 && x.Action == "ویرایش").ToListAsync();
        //var rs2 = res.GroupBy(x => x.ObjectId);
        //foreach (var item in rs2)
        //{
        //    if (item.Count() >= 2)
        //    {
        //        if (item.FirstOrDefault()?.Data != item.LastOrDefault()?.Data)
        //        {
        //            var lastUserData = DataConvertor.DeserializePostedUser(item?.FirstOrDefault()?.Data);
        //            var newUserData = DataConvertor.DeserializePostedUser(item?.LastOrDefault()?.Data);
        //            if (lastUserData.FirstName + " " + lastUserData.LastName != newUserData.FirstName + " " + newUserData.LastName)
        //            {
        //                var user = await FirstOrDefault(x => x.UserId == lastUserData.UserId);
        //                var userbooks = await _db.UserBooks.Where(x => x.UserId == lastUserData.UserId).ToListAsync();
        //                user.FirstName = lastUserData.FirstName;
        //                user.LastName = lastUserData.LastName;
        //                user.Name = lastUserData.FirstName + " " + lastUserData.LastName;
        //                user.Grade = lastUserData.Grade;
        //                user.Gender = lastUserData.Gender;
        //                user.HelliCode = lastUserData.HelliCode;
        //                user.MobileNumber = lastUserData.MobileNumber;
        //                _db.RemoveRange(userbooks);
        //                var usernewbooks = new List<UserBook>();
        //                foreach (var book in lastUserData?.BooksId ?? new List<int>())
        //                {
        //                    usernewbooks.Add(new UserBook()
        //                    {
        //                        BookId = book,
        //                        UserId = lastUserData.UserId
        //                    });
        //                }
        //                await _db.AddRangeAsync(usernewbooks);
        //                await Upsert(user);
        //            }
        //        }
        //    }
        //}



        // کسایی که از نسخه های قبلی اپ استفاده میکنن
        //var date = new DateTime(2023, 04, 28);
        //var res = await _db.BookReadLogs.Where(x => x.VersionType != "1.2.4" && x.DateReadBook >= date)
        //    .ToListAsync();

        //  دسترسی دانش آموزان که به کتاب پایه های دیگه دسترسی دارن این آیدی ها برای پایه ششم تنظیم شده
        //var user = await _db.UserBooks.Include(x=>x.User).OrderBy(x => x.Id).Where(x => x.User.Grade == 3 && x.User.IsDelete==false).Skip(0).Take(int.MaxValue).ToListAsync();
        //var usergroups = user.GroupBy(x => x.UserId);
        //var books = await _db.Books.Where(x => x.GroupId != 29).ToListAsync();
        //string numbers = "";
        //foreach (var item in usergroups)
        //{
        //    foreach (var book in books)
        //    {
        //        var res = item?.Any(x => x.BookId == book.Id);
        //        if (res.Value)
        //        {
        //            var olduserbook = user.FirstOrDefault(x => x.UserId == item?.FirstOrDefault()?.UserId && x.BookId == book.Id);
        //            _db.UserBooks.Remove(olduserbook);
        //            //var userbook = new UserBook()
        //            //{
        //            //    UserId = item.FirstOrDefault().UserId,
        //            //    BookId = book.Id
        //            //};
        //            //numbers += item?.FirstOrDefault()?.UserId + "-";
        //            //var notePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "User.txt");
        //            //if (File.Exists(notePath))
        //            //{
        //            //    await File.WriteAllTextAsync(notePath, numbers);
        //            //}
        //        }
        //    }

        //    await _db.SaveChangesAsync();
        //}
        //await _db.SaveChangesAsync();
        //var end = numbers;
        //}
        public async ValueTask DisposeAsync()
        {
            await _db.DisposeAsync();
        }
        //private async Task<UserSession> CreateTokenV4(User user)
        //{
        //    var token = Guid.NewGuid().ToString();
        //    var userSession = new UserSession()
        //    {
        //        DateTime = DateTime.UtcNow.AddHours(3.5),
        //        Token = token,
        //        UserId = user.UserId,
        //        Users = user
        //    };
        //    await _db.UserSessions.AddAsync(userSession);
        //    await _db.SaveChangesAsync();
        //    return userSession;
        //}
        //public async Task DisConnectAdvisor(int advisorId)
        //{
        //    var users = await GetAll(u => u.AdvisorId == advisorId);
        //    foreach (var user in users)
        //    {
        //        user.AdvisorId = null;
        //        await Upsert(user);
        //    }
        //}

        //public Task<int> GetUserReadLogsCount(DateTime? start, DateTime? end, Expression<Func<User, bool>> where = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ServiceMessage> ActivationV4(string mobile, string activeCode, string? helliCode)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
