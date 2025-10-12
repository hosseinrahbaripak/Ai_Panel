using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ai_Panel.Application.DTOs.AiChat;

public class AiChatApiDto
{
    public int Id { get; set; }
    public string UserMessage { get; set; }
    public string AiResponse { get; set; }
    public int BookId { get; set; }
    public int PartId { get; set; }
    public int? QuestionId { get; set; }
    public DateTime DateTime { get; set; }
}

public class UserAskFromAiChatApiDto
{
    public int PartId { get; set; }
    public int? QuestionId { get; set; }

    [MaxLength(1000)]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Message { get; set; }
}
public class AskHelliGptDto
{
    public string ai { get; set; }
    public string message { get; set; }
    public List<ChatHistoryDto> chat_history { get; set; }
    public string book_content { get; set; }
    public string model { get; set; }
    public string prompt { get; set; }
    public int n { get; set; }
    public float temperature { get; set; }
    public int max_tokens { get; set; }
    public float top_p { get; set; }
    public string[] stop { get; set; }
    public float presence_penalty { get; set; }
    public float frequency_penalty { get; set; }
}
public class AskHelliGptDtoV2
{
	public string ai { get; set; }
	public List<MessageHelliGptDto> messages { get; set; }
	public string model { get; set; }
	public string prompt { get; set; }
	public int n { get; set; }
	public float temperature { get; set; }
	public int max_tokens { get; set; }
	public float top_p { get; set; }
	public string[] stop { get; set; }
	public float presence_penalty { get; set; }
	public float frequency_penalty { get; set; }
}
public class MessageHelliGptDto
{
    public string Role { get; set; }
	public List<ContentHelliGptDto> Content { get; set; }
}
[JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
[JsonDerivedType(typeof(TextContentHelliGptDto), typeDiscriminator: "text")]
[JsonDerivedType(typeof(ImageContentHelliGptDto), typeDiscriminator: "image_url")]
public class ContentHelliGptDto
{
    public string Type { get; set; }
}
public class TextContentHelliGptDto : ContentHelliGptDto
{
	public string Text { get; set; }
}
public class ImageContentHelliGptDto : ContentHelliGptDto
{
	public string Image_Url { get; set; }
}
public class ChatHistoryDto
{
    public string Role { get; set; }
    public string Content { get; set; }
}
public class AnswerHelliGptDto
{
    public string[] response { get; set; }
    public Cost Cost { get; set; }
}
public class Cost
{
    public double Summarization_cost { get; set; }
    public double Request_cost { get; set; }
    public double Embedding_cost { get; set; }
}