using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_Panel.Domain
{
	public class ErrorLog
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(1500)]
		public string Message { get; set; }

		[MaxLength(1500)]
		public string? InnerMessage { get; set; }

		[MaxLength(50)]
		public Guid Token { get; set; }

		[MaxLength(50)]
		public string Action { get; set; }

		public DateTime DateTime { get; set; }
	}
}
