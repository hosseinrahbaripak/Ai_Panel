//using Live_Book.Application.Constants;
//using Live_Book.Application.Contracts.Persistence.EfCore;
//using Live_Book.Classes;
//using Live_Book.Domain;
//using Live_Book.Domain.Enum;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;

//namespace Live_Book.Areas.AdminArea.Controllers
//{
//    [ApiExplorerSettings(IgnoreApi = true)]
//    [PermissionChecker]
//    [Area("AdminArea")]
//    public class RoleInPagesController : Controller
//    {
//        public AdminLoggedIn? Admin {  get; set; }
//        public int ProjectId { get; set; }

//        private readonly IRole _role;
//        public RoleInPagesController(IRole role)
//        {
//            _role = role;
//        }

//        [Route("Admin/RoleInPages/Index/{roleId}")]
//        public async Task<IActionResult> Index(int roleId)
//        {
//            if (roleId == 1)
//            {
//                var adminUserName = User.Identity?.Name;
//                if (adminUserName != "developer")
//                {
//                    return Redirect("/NoPermission");
//                }
//            }
//            var checkAdmin = await CheckAdmin();
//            if (checkAdmin != null)
//                return checkAdmin;
//            ViewBag.RoleId = roleId;
//            ViewBag.RoleName = await _role.GetRoleNameById(roleId);
//            var model = await _role.GetAllRoleInPages(x => x.RoleId == roleId);
//            return View(model);
//        }

//        [Route("Admin/RoleInPages/Add/{roleId}")]
//        public async Task<IActionResult> Add(int roleId)
//        {
//            var checkAdmin = await CheckAdmin();
//            if (checkAdmin != null)
//                return checkAdmin;
//            List<Domain.Pages> pages;
//            if (ProjectId != 0)
//                pages = await _role.GetAllPages(x=> x.RolePages.Where(y => y.RoleId == Admin.RoleId).Any());
//            else
//                pages = await _role.GetAllPages();
//            var roleInPages = await _role.GetAllRoleInPages(x => x.RoleId == roleId);
//            var userPages = roleInPages.Select(x => x.Pages).ToList();
//            foreach (var item in userPages)
//            {
//                pages.Remove(item);
//            }
//            ViewBag.PageId = new SelectList(pages, "PageId", "PersianPageTitle");
//            ViewBag.PagesCount = pages.Count;
//            return PartialView();

//        }

//        [HttpPost]
//        [Route("Admin/RoleInPages/Add/{roleId}")]
//        public async Task<IActionResult> Add(int roleId, RolesInPages roleInPages)
//        {
//            var checkAdmin = await CheckAdmin();
//            if (checkAdmin != null)
//                return checkAdmin;
//            ModelState.Remove("Role");
//            ModelState.Remove("Pages");
//            if (ModelState.IsValid && roleInPages.PageId != 0)
//            {
//                var isExist = await _role.FirstOrDefaultRoleInPages(x => x.RoleId == roleId && x.PageId == roleInPages.PageId);
//                if (isExist == null)
//                {
//                    await _role.AddRoleInRolePages(roleInPages);
//                    return Json(new { status = "success" });
//                }
//                else
//                {
//                    return Json(new { status = "objectExist" });
//                }
//            }
//            else
//            {
//                return Json(new { status = "objectNotValid" });
//            }
//        }

//        [Route("Admin/RoleInPages/Edit/{roleInPageId}")]
//        public async Task<IActionResult> Edit(int roleInPageId)
//        {
//            var checkAdmin = await CheckAdmin();
//            if (checkAdmin != null)
//                return checkAdmin;
//            var res = await _role.FindRoleInPage(roleInPageId);
//            if (res == null) return NotFound();
//            ViewBag.PageName = res.Pages.PersianPageTitle;
//            return PartialView(res);
//        }

//        [HttpPost]
//        [Route("Admin/RoleInPages/Edit/{roleInPageId}")]
//        public async Task<IActionResult> Edit(int roleInPageId, RolesInPages roleInPage)
//        {
//            var checkAdmin = await CheckAdmin();
//            if (checkAdmin != null)
//                return checkAdmin;
//            ModelState.Remove("Role");
//            ModelState.Remove("Pages");
//            var res = await _role.FindRoleInPage(roleInPageId);
//            if (ModelState.IsValid)
//            {
//                res.Add = roleInPage.Add;
//                res.Edit = roleInPage.Edit;
//                res.Delete = roleInPage.Delete;
//                res.Visit = roleInPage.Visit;
//                await _role.UpdateRoleInPage(res);
//                return Json(new { status = "success" });
//            }
//            else
//            {
//                return Json(new { status = "objectNotValid" });
//            }
//        }

//        [Route("Admin/RoleInPages/Delete")]
//        [HttpPost]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var checkAdmin = await CheckAdmin();
//            if (checkAdmin != null)
//                return checkAdmin;
//            try
//            {
//                await _role.DeleteRoleInPage(id);
//                return Json(new
//                {
//                    errorId = 0,

//                });
//            }
//            catch (Exception)
//            {
//                return Json(new
//                {
//                    errorId = -1,
//                    errorTitle = "امکان حذف گروه وجود ندارد."
//                });
//            }
//        }
//        public async Task<IActionResult?> CheckAdmin()
//        {
//            Admin ??= await _adminHelper.GetAdminLoggedIn();
//            if (Admin == null) return RedirectToPage(Urls.SignOut);
//            if (Admin.AdminType == AdminTypeIdEnum.ProjectManager)
//                ProjectId = Admin.AdminProfileId;
//            else if(!(Admin.AdminType == AdminTypeIdEnum.GeneralAdmin || Admin.AdminType == AdminTypeIdEnum.ProjectManager))
//                return RedirectToPage(Urls.NoPermission);
//            return null;
//        }
//    }
//}
