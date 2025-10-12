using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Live_Book.Domain;

public class AiModel
{
    [Key]
    public int Id { get; set; }
    [MaxLength(250)]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Title { get; set; }
    public int? ParentId { get; set; }
    [ForeignKey(nameof(ParentId))]
    public AiModel? Parent { get; set; }
    public bool IsDelete { get; set; }
    public List<AiPlatform> Platforms { get; set; }
    public List<TestAiConfig>? TestAiConfigs { get; set; }
}