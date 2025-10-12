using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Live_Book.Domain
{
	public class ApiRequestLog
	{
		[Key]
		public int Id { get; set; }
		[MaxLength(2000)]
		public string? Header { get; set; }
		[MaxLength(500)]
		public string Url { get; set; }
		public string? Body { get; set; }
		public string? Response { get; set; }
		public DateTime? Date { get; set; }
		public int? UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public User? User { get; set; }
	}
}