using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Domain;

namespace Ai_Panel.Application.Contracts.Persistence.EfCore
{
    public interface IContractRepository:IGenericRepository<ContractTemplate>
    {}
}
