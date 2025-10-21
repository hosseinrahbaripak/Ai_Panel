using System.ComponentModel.DataAnnotations;

namespace Ai_Panel.Domain
{
    public class AiConfigGroup
    {
        [Key]
        public int Id { get; set; }
        public List<AiConfig> AiConfig { get; set; }
        public bool IsDelete { get; set; }
    }
}
