using Ai_Panel.Application.Tools;

namespace Ai_Panel.Application.DTOs.AiChat;
public class AiChatDashboardDto
{
	public List<AiChatLogDto> AiChatLogs { get; set; }
	public int AiChatLogsCount => AiChatLogs.Count();
	public double AiChatLogsTotalCost => AiChatLogs?.Sum(x => x.Cost) ?? 0;
	public string OrderBy { get; set; }

}
public class WordsCountIdTitle : IdTitleTimeBased
{
	public string IdString { get; set; }
	public override int Id => Utility.CountWords(IdString);
}
public class UserAiChatLogsFilter
{
	public int BookId { get; set; }
	public int PartId { get; set; }
	public int UserId { get; set; }
	public string OrderBy { get; set; }
}