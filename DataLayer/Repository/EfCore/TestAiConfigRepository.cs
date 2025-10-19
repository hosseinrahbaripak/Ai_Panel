using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Domain;
using Ai_Panel.Persistence.Configurations;

namespace Ai_Panel.Persistence.Repository.EfCore
{
    public class TestAiConfigRepository : GenericRepository<TestAiConfig>
    {
        private readonly AiPanelContext _context;
        public TestAiConfigRepository(AiPanelContext context) : base(context)
        {
            _context = context;
        }
    }
}
