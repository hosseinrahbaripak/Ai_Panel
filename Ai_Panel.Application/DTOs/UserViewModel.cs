using Ai_Panel.Domain;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ai_Panel.Application.DTOs
{
	public class AddExcelUserViewModel
	{
		[Required] public int GradeId { get; set; }
		public List<int>? ParentUserTagId { get; set; }
		public List<int>? UserTagId { get; set; }
		public List<int>? BookId { get; set; }
		[Required]
		[DisplayName("پروژه")]
		public int? ProjectId { get; set; }
		[Required] public required IFormFile Excel { get; set; }
	}
	public class UpdateUserExcelViewModel
	{
		[Required] public IFormFile Excel { get; set; }
	}
	public class ImportExcelUserReport
	{
		public string? MobileNumber { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string Reason { get; set; }
	}
	public class UpdateUserExcelReport
	{
		public string? OldMobileNumber { get; set; }
		public string? NewMobileNumber { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string Reason { get; set; }
	}
	public class UserInputViewModel
	{
		[MaxLength(150)]
		[Display(Name = "نام")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string FirstName { get; set; }

		[MaxLength(150)]
		[Display(Name = "نام خانوادگی")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string LastName { get; set; }

		public string? Name => $"{FirstName} {LastName}";

		[Display(Name = "شماره همراه")]
		[MaxLength(11)]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string MobileNumber { get; set; }


		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[Display(Name = "جنسیت")]
		public int Gender { get; set; }



	}
	public class FullInfoUser
	{
		public Domain.User User { get; set; }
		public int RequestLoginsCount { get; set; }
		public int BookReadLogsCount { get; set; }
	}
	public class UserCmpProfileModel
	{
		public string MobileNumber { get; set; }

		[MaxLength(150)]
		[Display(Name = "نام")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string FirstName { get; set; }

		[MaxLength(150)]
		[Display(Name = "نام خانوادگی")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string LastName { get; set; }

		[Display(Name = "پایه")]
		public int GradeId { get; set; }
	}
}