//using Live_Book.Application.Constants;
//using Live_Book.Application.Contracts.Persistence.EfCore;
//using Live_Book.Application.Features.Roles.Request.Queries;
//using Live_Book.Classes;
//using Live_Book.Domain;
//using Live_Book.Domain.Enum;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Linq.Expressions;

//namespace Live_Book.Areas.AdminArea.Controllers
//{
//    [ApiExplorerSettings(IgnoreApi = true)]
//    [PermissionChecker]
//    [Area("AdminArea")]
//    public class RolesController : Controller
//    {
//        private readonly IRole _role;
//        private readonly IAdminTypeRepository _adminType;
//		private readonly IProjectRepository _project;
//        private readonly AdminHelper _adminHelper;
//        private readonly IMediator _mediator;
//        public AdminLoggedIn Admin { get; set; } = null;
//		public RolesController(IRole role, IAdminTypeRepository adminTypeRepository, 
//            IProjectRepository projectRepository, AdminHelper adminHelper, IMediator mediator)
//        {
//            _role = role;
//            _adminType = adminTypeRepository;
//            _project = projectRepository;
//            _adminHelper = adminHelper;
//            _mediator = mediator;
//        }

//        [Route("Admin/Roles/Index")]
//        public async Task<IActionResult> Index(int ParentId = 0)
//        {
//            Admin ??= await _adminHelper.GetAdminLoggedIn();
//            if (Admin == null)
//                return RedirectToPage(Urls.SignOut);

//            if (Admin.AdminType == AdminTypeIdEnum.ProjectManager)
//                ParentId = Admin.RoleId;

//            else if(Admin.AdminType == AdminTypeIdEnum.Advisor)
//                return RedirectToPage(Urls.SignOut);

//            Expression<Func<Role, bool>> where = null;
//            if (ParentId != 0)
//            {
//                where = x => (x.ParentId == ParentId);
//            }
//            else
//            {
//                where = x => (x.ParentId ==null);
//            }
//            ViewBag.ParentId = ParentId;
//            var model = await _role.GetAll(where);
//            return View(model);
//        }

//        [Route("Admin/Roles/Add")]
//        public async Task<IActionResult> Add(int ParentId = 0)
//        {
//            return await FillDropDown(ParentId :ParentId);
//        }

//        [HttpPost]
//        [Route("Admin/Roles/Add")]
//        public async Task<IActionResult> Add(Role role, int ParentId = 0)
//        {
//            Admin = await _adminHelper.GetAdminLoggedIn();
//            if (ModelState.IsValid)
//            {
//                if (Admin == null)
//					return RedirectToPage(Urls.SignOut);

//				if (ParentId != 0)
//				{
//					role.ParentId = ParentId;
//                }
//                else
//                {
//                    role.ParentId = null;
//                }
//				if (role.AdminTypeId == null || role.AdminTypeId == 0)
//                {
//                    return Json(new { status = "adminTypeRequired" });
//                }
//                var isExist = await _role.FirstOrDefault(x => x.RoleTitle == role.RoleTitle && x.ParentId == role.ParentId);
//                if (isExist == null)
//                {
//                    await _role.AddRole(role);
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

//        [Route("Admin/Roles/Edit/{id}")]
//        public async Task<IActionResult> Edit(int id, int ParentId = 0)
//        {
//            var role = await _role.FindRoleById(id);
//            if (role == null) return NotFound();
//            return await FillDropDown(role, ParentId : ParentId);
//        }

//        [HttpPost]
//        [Route("Admin/Roles/Edit/{id}")]
//        public async Task<IActionResult> Edit(int id, Role role, int ParentId = 0)
//        {
//            Admin = await _adminHelper.GetAdminLoggedIn();
//            if (Admin == null)
//				return RedirectToPage(Urls.SignOut);

//			if (role.AdminTypeId == null || role.AdminTypeId == 0)
//            {
//                return Json(new { status = "adminTypeRequired" });
//            }
//            if (ParentId != 0)
//            {
//                role.ParentId = ParentId;
//            }
//            else
//            {
//                role.ParentId = null;
//            }
//            if (ModelState.IsValid)
//            {
//                var isExist = await _role.FirstOrDefault(x => x.RoleTitle == role.RoleTitle && x.ParentId == role.ParentId && x.RoleId != role.RoleId);
//                if (isExist == null)
//                {
//                    await _role.UpdateRole(role);
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

//        [Route("Admin/Roles/Delete")]
//        [HttpPost]
//        public async Task<IActionResult> Delete(int id)
//        {
//            try
//            {
//                var role = await _role.FindRoleById(id);
//                if (role?.AdminRoles?.Count > 0)
//                {
//                    return Json(new
//                    {
//                        errorId = -1,
//                        errorTitle = "لطفا ابتدا تمامی ادمین هایی که این نقش را دارند را حذف کنید"
//                    });
//                }
//                if (role != null && role.RoleTitle != "developer")
//                {
//                    await _role.RemoveRole(id);
//                    return Json(new
//                    {
//                        errorId = 0,

//                    });
//                }
//                else
//                {
//                    return Json(new
//                    {
//                        errorId = -1,
//                        errorTitle = "امکان حذف نقش وجود ندارد."
//                    });
//                }

//            }
//            catch (Exception)
//            {
//                return Json(new
//                {
//                    errorId = -1,
//                    errorTitle = "امکان حذف نقش وجود ندارد."
//                });
//            }
//        }
//		private async Task<IActionResult> FillDropDown(Role role=null, int ParentId=0)
//        {
//            Admin = await _adminHelper.GetAdminLoggedIn();
//            SelectList adminTypes = null;
//            Expression<Func<AdminType, bool>> where = null;
//            if (Admin.AdminType == AdminTypeIdEnum.GeneralAdmin && ParentId == 0)
//            {
//                where = null;
//            }
//            else {
//                where = x => x.Id != (int)AdminTypeIdEnum.GeneralAdmin && x.Id != (int)AdminTypeIdEnum.ProjectManager;
//            }
//            adminTypes = new SelectList(await _adminType.GetAll(where), "Id", "Title", role?.AdminTypeId);
//            ViewBag.AdminTypes = adminTypes;
//            ViewBag.ParentId = ParentId;
//            return PartialView(role);
//		}
//        [Route("Admin/Roles/Index/GetByProject/")]
//        [HttpGet]
//        public async Task<IActionResult> GetProjectRoles(string projectId, int adminType = (int)AdminTypeIdEnum.Advisor)
//        {
//			if (string.IsNullOrEmpty(projectId))
//			{
//				return Json(null);
//			}
//			var idStrings = projectId.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
//			var projectIds = new List<int>();
//			foreach (var idStr in idStrings)
//			{
//				if (!int.TryParse(idStr, out int id))
//				{
//					return Json(null);
//				}
//				projectIds.Add(id);
//			}
//			var roles = await _mediator.Send(new GetRoleByProjectRequest()
//            {
//                ProjectIds = projectIds,
//                AdminType = (AdminTypeIdEnum)adminType
//            });
//            return Json(
//                roles
//            );
//        }
//    }   
//}
