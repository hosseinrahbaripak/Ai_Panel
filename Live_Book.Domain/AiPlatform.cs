using Live_Book.Domain.Common;

namespace Live_Book.Domain;
public class AiPlatform
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ApiEndpoint { get; set; }
    public List<AiModel> Models { get; set; }
    public List<AiConfig> Configs { get; set; }
    public bool IsDelete { get; set; }
}
