using System.ComponentModel.DataAnnotations;

namespace Ai_Panel.Application.DTOs.Contract
{
    public class UpserContractDto
    {
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title {  get; set; }

        [Display(Name = "محتوا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Content {  get; set; }
    }
}
