using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Classes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Live_Book.Pages.Admin.Home
{
    [PermissionChecker]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IUser _user;


        public IndexModel(IMediator mediator, IUser user
        )
        {
            _mediator = mediator;
            _user = user;
           
        }

        //public DateValueDto UsersUsages { get; set; }
        //public TitleValue UsersUsagesPerHour { get; set; }
        //public TitleValue UsersUsagesPerTag { get; set; }
        //public int UsersCount { get; set; }
        //public int TodayBookReadLogCount { get; set; }
        //public double InstallationRatio { get; set; }
        //public List<int> ProjectIds { get; set; }
        //public int ParentAdvisorId { get; set; }
        //public List<IdTitle> AdvisorsFilter { get; set; }
        //public int AdvisorId { get; set; }

        public async Task<IActionResult> OnGet()
        {
            return Page();
   //         var res = await FilterReq();
			//await FillDropDown();
			//return res;
        }

        //     private async Task<IActionResult> FilterReq()
        //     {
        //         var admin = await _adminHelper.GetAdminLoggedIn();
        //         if (admin == null) return RedirectToPage(Urls.SignOut);
        //         int? profileId = admin.AdminProfileId;
        //         if (profileId is null or 0)
        //             return Redirect(Urls.SignOut);
        //         ProjectIds = admin.ProjectIds;
        //         if (admin.AdminType == AdminTypeIdEnum.ParentAdvisor)
        //         {
        //             ParentAdvisorId = admin.AdminProfileId;
        //         }
        //         else if (admin.AdminType == AdminTypeIdEnum.Advisor)
        //         {
        //             AdvisorId = admin.AdminProfileId;
        //         }
        //         if (ParentAdvisorId > 0 && ProjectIds?.Count > 0)
        //         {
        //             AdvisorsFilter = await _mediator.Send(new GetAdvisorsByRequest() { ParentId = ParentAdvisorId , ProjectIds = ProjectIds, ForChild = true });
        //         }
        //         UsersUsages = await _mediator.Send(new GetUserUsageRequest()
        //         {
        //             ModelFilter = new UserUsageFilter()
        //             {
        //                 ProjectIds = ProjectIds,
        //                 AdvisorId = AdvisorId,
        //                 ParentAdvisorId = ParentAdvisorId,
        //                 AdvisorsIdOfParent = AdvisorsFilter?.Select(x => x.Id).ToList(),
        //             },
        //         });
        //         ReadLogsPerBook = await _mediator.Send(new GetReadLogsPerBookRequest()
        //         {
        //             ModelFilter = new ReadLogsPerBookFilter()
        //             {
        //                 ProjectIds = ProjectIds,
        //                 AdvisorId = AdvisorId,
        //                 ParentAdvisorId = ParentAdvisorId,
        //                 AdvisorsIdOfParent = AdvisorsFilter?.Select(x => x.Id).ToList(),
        //             },
        //         });
        //         UsersPer = await _mediator.Send(new GetUsersPerRequest()
        //         {
        //             ModelFilter= new UsersPerFilter()
        //             {
        //                 ProjectIds = ProjectIds,
        //                 AdvisorId = AdvisorId,
        //                 ParentAdvisorId = ParentAdvisorId,
        //                 AdvisorsIdOfParent = AdvisorsFilter?.Select(x => x.Id).ToList(),
        //             }
        //         });
        //         UsersUsagesPerHour = await _mediator.Send(new GetUserUsagePerHourRequest()
        //         {
        //             ModelFilter = new UserUsagePerHourFilter()
        //             {
        //                 ProjectIds = ProjectIds,
        //                 AdvisorId = AdvisorId,
        //                 ParentAdvisorId = ParentAdvisorId,
        //                 AdvisorsIdOfParent = AdvisorsFilter?.Select(x => x.Id).ToList(),
        //             }
        //         });
        //         UsersUsagesPerTag = await _mediator.Send(new GetUsersUsagePerTagRequest()
        //         {
        //             ModelFilter = new UserUsageFilter()
        //             {
        //                 ProjectIds = ProjectIds,
        //                 AdvisorId = AdvisorId,
        //                 ParentAdvisorId = ParentAdvisorId,
        //                 AdvisorsIdOfParent = AdvisorsFilter?.Select(x => x.Id).ToList(),
        //             }
        //         });
        //         Stats = await _mediator.Send(new GetUsersInstallationStasRequest()
        //         {
        //             ModelFilter = new ChartsBaseFilter()
        //             {
        //                 ProjectIds = ProjectIds,
        //                 AdvisorId = AdvisorId,
        //                 ParentAdvisorId = ParentAdvisorId,
        //                 AdvisorsIdOfParent = AdvisorsFilter?.Select(x => x.Id).ToList(),
        //             }
        //         });

        //         UsersCount = await _user.GetCount(x => !x.IsDelete &&
        //             (ProjectIds == null || ProjectIds.Count == 0 || ProjectIds.Contains(x.ProjectId.Value)) &&
        //             (AdvisorsFilter == null || (x.AdvisorId.HasValue && AdvisorsFilter.Select(a => a.Id).Contains(x.AdvisorId.Value))) &&
        //             (AdvisorId == 0 || x.AdvisorId == AdvisorId)
        //         );
        //         TodayBookReadLogCount = await _readBookLog.GetCount(
        //             x =>
        //             (x.EndDateReadBook != null) &&
        //             (!x.User.IsDelete) && (!x.Book.IsDelete) &&
        //             x.DateReadBook >= DateTime.UtcNow.AddHours(3.5).Date &&
        //             (ProjectIds == null || ProjectIds.Count == 0 || ProjectIds.Contains(x.User.ProjectId.Value)) &&
        //             (AdvisorsFilter == null || (x.User.AdvisorId.HasValue && AdvisorsFilter.Select(a => a.Id).Contains(x.User.AdvisorId.Value))) &&
        //             (AdvisorId == 0 || x.User.AdvisorId == AdvisorId)
        //         );
        //         var totalInstallations = await _user.GetTotalInstallationCount(x =>
        //             !x.Users.IsDelete &&
        //             ( ProjectIds == null || ProjectIds.Count == 0 || ProjectIds.Contains(x.Users.ProjectId.Value)) &&
        //             (AdvisorsFilter == null || (x.Users.AdvisorId.HasValue && AdvisorsFilter.Select(a => a.Id).Contains(x.Users.AdvisorId.Value))) &&
        //             (AdvisorId == 0 || x.Users.AdvisorId == AdvisorId)
        //         );
        //         InstallationRatio = UsersCount > 0 ? Math.Round(Convert.ToDouble(totalInstallations) / UsersCount * 100 ,2) : 0;
        //         return Page();
        //     }
        //     private async Task FillDropDown()
        //     {
        //         var selectedReadTypeId = (int)ReadType.ReadTime;
        //         var selectedTimeTypeId = (int)TimeType.Monthly;
        //         var selectedByTypeId = (int)ByTypeEnum.Grade;
        //         ViewData["Books"] = new SelectList(await _books.GetAllBooks(x=> !x.IsDelete), "Id", "Name");
        //         ViewData["ReadType"] = new SelectList(Utility.GetReadTypeIdTitle(), "Id", "Title", selectedReadTypeId);
        //         ViewData["TimeType"] = new SelectList(Utility.GetTimeTypeIdTitle(), "Id", "Title", selectedTimeTypeId);
        //         ViewData["ByType"] = new SelectList(Utility.GetByTypeIdTitle(), "Id", "Title", selectedByTypeId);
        //         ViewData["BookGroups"] = new SelectList(
        //             await _bookGroups.GetAllGroupsAsync(
        //                 bc => bc.Book != null && bc.Book.Where(b => !b.IsDelete).Any())
        //             , "GroupId", "GroupTitle");
        //         ViewData["Grades"] = new SelectList(await _grade.GetAll(), "GradeId", "GradeTitle");
        //         ViewData["Projects"] = ProjectIds == null || ProjectIds.Count == 0 ? new SelectList(await _project.GetAll(), "Id", "Title") : null;
        //ViewData["ParentUserTags"] = new SelectList(await _tag.GetAll(x =>
        //	 x.ParentId == null &&
        //	 (ProjectIds == null || ProjectIds.Count == 0
        //	 ? x.ProjectId == null
        //	 : ProjectIds.Contains(x.ProjectId.Value) || x.ProjectId == null)),
        // "Id", "Title");
        //         ViewData["ParentAdvisors"] = AdvisorId == 0 && ParentAdvisorId == 0 && ProjectIds?.Count > 0 ?
        //             new SelectList(await _mediator.Send(new GetAdvisorsByRequest() { ProjectIds = ProjectIds, ForChild = false }), "Id", "Title")
        //             : null;
        //         ViewData["Advisors"] = AdvisorId == 0 &&
        //             AdvisorsFilter != null ? new SelectList(AdvisorsFilter, "Id", "Title") : null;
        //     }
    }
}
