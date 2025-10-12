namespace Ai_Panel.Application.DTOs.TestAiConfig;
public class TestAiConfigDashboardDto
{
	public List<TestAiConfigLogDto> TestAiConfigLogs { get; set; }
	public int TestAiConfigLogsCount => TestAiConfigLogs.Count();
	public double TestAiConfigLogsTotalCost => TestAiConfigLogs?.Sum(x => x.Cost) ?? 0;
	public string OrderBy { get; set; }
}