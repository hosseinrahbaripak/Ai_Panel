using System.ComponentModel.DataAnnotations;

namespace Live_Book.Domain.Enum;

public enum AppSystemTypeEnum : byte
{
    [Display(Name = "اندروید")]
    Android,
    [Display(Name = "ios")]
    Ios,
    [Display(Name = "ویندوز")]
    Windows
}