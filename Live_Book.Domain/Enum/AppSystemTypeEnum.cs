using System.ComponentModel.DataAnnotations;

namespace Ai_Panel.Domain.Enum;

public enum AppSystemTypeEnum : byte
{
    [Display(Name = "اندروید")]
    Android,
    [Display(Name = "ios")]
    Ios,
    [Display(Name = "ویندوز")]
    Windows
}