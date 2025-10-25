using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Domain;
using Ai_Panel.Persistence.Configurations;

namespace Ai_Panel.Persistence.Repository.EfCore
{
    public class ContractRepository : GenericRepository<ContractTemplate>, IContractRepository
    {
        public ContractRepository(AiPanelContext context) : base(context)
        {
        }
    }
}
