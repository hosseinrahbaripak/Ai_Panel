using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ai_Panel.Domain;

public class UserAiChatLog
{
    [Key]
    public int Id { get; set; }
    public DateTime DateTime { get; set; }

    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string UserMessage { get; set; }
    public bool AiCouldResponse { get; set; }
    public string? AiResponse { get; set; }
    public double SummarizationCost { get; set; }
    public double RequestCost { get; set; }
    public double EmbeddingCost { get; set; }
    #region Relation
    public int UserServiceId { get; set; }
    public UserService UserService { get; set; }

    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    #endregion
}