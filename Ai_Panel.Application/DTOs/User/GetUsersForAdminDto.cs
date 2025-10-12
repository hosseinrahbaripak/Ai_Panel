using Ai_Panel.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Ai_Panel.Application.DTOs.User;

public class GetUsersForAdminDto
{
    public int UserId { get; set; }

    [Display(Name = "نام")]
    public string? Name { get; set; }

    [Display(Name = "نام")]
    public string? FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    public string? LastName { get; set; }

    [MaxLength(11)]
    [Display(Name = "شماره همراه")]
    public string MobileNumber { get; set; }


    [Display(Name = "جنسیت")]
    public int? Gender { get; set; }

    [Display(Name = "تاریخ ثبت نام")]
    public DateTime DateTime { get; set; }

    [Display(Name = "تاریخ آپدیت")]
    public DateTime UpdateDateTime { get; set; }

    public bool IsDelete { get; set; }
    public DateTime? FirstLoginToApp { get; set; }
    public DateTime? LastLoginToApp { get; set; }

}