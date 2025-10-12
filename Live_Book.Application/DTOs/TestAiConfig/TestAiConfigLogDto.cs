using Live_Book.Application.DTOs.AiChat;
using System.ComponentModel;
using System.Text.Json;

namespace Live_Book.Application.DTOs.TestAiConfig;
public class TestAiConfigLogDto : AiChatLogDto
{
	[DisplayName("بیشترین تعداد کلمات خروجی")]
	public int MaxTokens { get; set; }
	[DisplayName("پرامپت")]
	public string Prompt { get; set; }
	[DisplayName("خلاقیت در پاسخگویی")]
	public float Temperature { get; set; }
	[DisplayName("کنترل تنوع پاسخ بر اساس احتمال")]
	public float TopP { get; set; }
	[DisplayName("خلاقیت در ساخت جمله")]
	public float PresencePenalty { get; set; }
	[DisplayName("جلوگیری از تکرار کلمه در جمله")]
	public float FrequencyPenalty { get; set; }
	public string StopJson { get; set; }

	[DisplayName("توقت پاسخگویی در کلمات خاص")]
	public string Stop => string.Join(", ", JsonSerializer.Deserialize<string[]>(StopJson) ?? new string[0]);
}
