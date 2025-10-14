using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ai_Panel.Domain;

public class AiModel
{
    [Key]
    public int Id { get; set; }
    [MaxLength(250)]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Title { get; set; }
    public double? InputPrice { get; set; }
    public double? CachedInputPrice { get; set; }
    public double? OutputPrice { get; set; }
    public int? ParentId { get; set; }
    [ForeignKey(nameof(ParentId))]
    public AiModel? Parent { get; set; }
    public bool IsDelete { get; set; }
    public List<AiPlatform> Platforms { get; set; }
}