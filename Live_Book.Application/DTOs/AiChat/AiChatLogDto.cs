using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace Live_Book.Application.DTOs.AiChat;
public class AiChatLogDto
{
	[DisplayName("نام")]
	public string Name { get; set; }
	[DisplayName("کتاب")]
	public string Book {  get; set; }
	[DisplayName("فصل")]
	public string Part { get; set; }
	public int AiChatId { get; set; }
    [DisplayName("درخواست")]
    public string UserMessage { get; set; }
    [DisplayName("پاسخ")]
    public string AiResponse { get; set; }
    [DisplayName("تاریخ")]
    public DateTime Date { get; set; }
    [DisplayName("هزینه")]
    public double Cost { get; set; }
    [DisplayName("وضعیت")]
    public bool AiCouldResponse { get; set; }
    [DisplayName("هوش مصنوعی")]
    public string Ai { get; set; }
    [DisplayName("مدل")]
    public string AiModel { get; set; }
    [DisplayName("نسخه")]
    public string Version { get; set; }
}

