using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_Panel.Domain
{
    public class ContractTemplate
    {
        [Key]
        public int Id {get;set;}
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsDelete { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public List<AiConfig>? AiConfigs { get; set; }
        public List<TestAiConfig>? TestAiConfigs { get; set; }
    }
}
