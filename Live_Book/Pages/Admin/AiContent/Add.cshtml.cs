using System.Text.Json;
using DocumentFormat.OpenXml.Office2010.Excel;
using Live_Book.Application.Constants;
using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Application.DTOs.AiConfig;
using Live_Book.Application.DTOs.AiContent;
using Live_Book.Application.Features.AiConfig.Request.Command;
using Live_Book.Application.Features.AiContent.Request.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Live_Book.Pages.Admin.AiContent
{
    public class AddModel( IMediator mediator, IAiConfigRepository aiConfigRepository) : PageModel
    {
        #region Model
        public string Error { get; set; }
        [BindProperty]
        public AiContentUpsertDto AiContentUpsertDto { get; set; }
        #endregion

        public async Task<IActionResult> OnGet()
        {
            return Page();
            //await FillDropDown();
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
                //if (resAdd.ErrorId == 0)
                //{
                //    var bookPartModel = await bookPart.GetPartById(AiContentUpsertDto.PartId);
                //    bookPartModel.AiContentId = (int)resAdd.Result;
                //    await bookPart.UpdatePart(bookPartModel);
                //    return Redirect(Urls.AiContent);
                //}
                Error = resAdd.ErrorTitle;
            }
            //await FillDropDown();
            return Page();
        }

        //#region Utility
        //private async Task FillDropDown()
        //{
        //    AiContentUpsertDto ??= new AiContentUpsertDto();
        //    var bookGroups = await bookGroup.GetAllGroupsAsync(bc => bc.Book != null && bc.Book.Any(b => !b.IsDelete));
        //    var books = await book.GetAllBooks(x => !x.IsDelete);
        //    var bookParts = await bookPart.GetBookParts(x => !x.IsDelete && x.AiContentId == null);
        //    ViewData["BookGroup"] = new SelectList(bookGroups, "GroupId", "GroupTitle");
        //    ViewData["Books"] = JsonSerializer.Serialize(books.Select(x => new
        //    {
        //        Id = x.Id,
        //        GroupId = x.GroupId,
        //        Title = x.Name
        //    }));
        //    ViewData["BookParts"] = JsonSerializer.Serialize(bookParts.Select(x => new
        //    {
        //        Id = x.PartId,
        //        BookId = x.BookId,
        //        Title = x.PartName
        //    }));
        //    var aiConfigs = await aiConfigRepository.GetAll(x => !x.IsDelete);
        //    ViewData["AiConfigs"] = new SelectList(aiConfigs, "Id", "Title");
        //}
        //#endregion
    }
}
