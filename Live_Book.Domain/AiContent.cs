using Ai_Panel.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ai_Panel.Domain;

public class AiContent : BaseEntity
{
    public string Content { get; set; }
}