using Ai_Panel.Domain.Common;

namespace Ai_Panel.Domain;
public class AiPlatform
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ApiEndpoint { get; set; }
    public List<AiModel> Models { get; set; }
    public List<AiConfig> Configs { get; set; }
    public bool IsDelete { get; set; }
}
