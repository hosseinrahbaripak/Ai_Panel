using Live_Book.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Live_Book.Domain;

public class AiContent : BaseEntity
{
    public string Content { get; set; }
}