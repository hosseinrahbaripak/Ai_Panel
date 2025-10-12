using System.ComponentModel.DataAnnotations;

namespace Ai_Panel.Domain.Enum;

public enum AdminTypeIdEnum
{
    [Display(Name = "ادمین کل")]
    GeneralAdmin = 1,
    [Display(Name = "مدیر پروژه")]
    ProjectManager = 2,
    [Display(Name = "مشاور")]
    Advisor = 3,
    [Display(Name = "سر مشاور")]
    ParentAdvisor = 4,
    [Display(Name = "سر پرست")]
    Supervisor = 5,
}