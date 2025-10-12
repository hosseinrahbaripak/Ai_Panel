using System.Text.Json;
using DocumentFormat.OpenXml.Office2010.Excel;
using Live_Book.Application.Constants;
using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Application.DTOs.AiContent;
using Live_Book.Application.Features.AiContent.Request.Command;
using Live_Book.Application.Features.AiContent.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Live_Book.Pages.Admin.AiContent
{
    [PermissionChecker]
    public class EditModel( IMediator mediator, IAiConfigRepository aiConfigRepository) : PageModel
    {
        #region Model
        public string Error { get; set; }
        [BindProperty]
        public AiContentUpsertDto AiContentUpsertDto { get; set; }
        #endregion

        public async Task<IActionResult> OnGet(int id)
        {
            //var res = await FillDropDown(id);
            //if (res != null)
            //    return res;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("AiContentUpsertDto.Part");
            ModelState.Remove("AiContentUpsertDto.Book");
            if (ModelState.IsValid)
            {
                var resAdd = await mediator.Send(new UpsertAiContentRequest()
                {
                    Model = AiContentUpsertDto
                });
                if (resAdd.ErrorId == 0)
                    return Redirect(Urls.AiContent);
                Error = resAdd.ErrorTitle;
            }
            //await FillDropDown(AiContentUpsertDto.Id);
            return Page();
        }

        //#region Utility
        //private async Task<IActionResult?> FillDropDown(int id)
        //{
        //    AiContentUpsertDto = await mediator.Send(new GetAiContentRequest()
        //    {
        //        Id = id
        //    });
        //    if (AiContentUpsertDto == null)
        //        return NotFound();
        //    AiContentUpsertDto ??= new AiContentUpsertDto();
        //    var aiConfigs = await aiConfigRepository.GetAll(x => !x.IsDelete);
        //    ViewData["AiConfigs"] = new SelectList(aiConfigs, "Id", "Title", AiContentUpsertDto.AiConfigId);
        //    return null;
        //}
        //#endregion
    }
}
