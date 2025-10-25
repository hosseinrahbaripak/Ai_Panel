using Ai_Panel.Application.Constants;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.Features.Contract.Request.Command;
using Ai_Panel.Domain;
using Amazon.Runtime.Internal.Util;
using MediatR;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.Contract.Handler.Command
{
    public class UpserContractHandler : IRequestHandler<UpsertContractCommand, ServiceMessage>
    {
        private readonly IContractRepository _contract;
        private readonly IErrorLog _errorLog;
        public UpserContractHandler(IContractRepository contract, IErrorLog errorLog)
        {
            _contract = contract;
            _errorLog = errorLog;
        }


        public async Task<ServiceMessage> Handle(UpsertContractCommand request, CancellationToken cancellationToken)
        {
            if (request.Contract.Id == 0)
            {
                ContractTemplate contract = new ContractTemplate()
                {
                    Content = request.Contract.Content,
                    Title = request.Contract.Title,
                    IsDelete = false,
                    DateTime = DateTime.UtcNow.AddHours(3.5),
                    UpdatedDateTime = DateTime.UtcNow.AddHours(3.5)
                };
                try
                {
                    await _contract.Add(contract);
                    return new ServiceMessage()
                    {
                        ErrorId = 0,
                        ErrorTitle = null,
                        Result = SystemMessages.Success
                    };

                }
                catch (Exception ex)
                {
                    await _errorLog.Add(ex.Message, ex.InnerException?.Message ?? "", "Add new Contract");
                    return new ServiceMessage()
                    {
                        ErrorId = -1,
                        ErrorTitle = SystemMessages.ErrorAddContract,
                        Result = null
                    };
                }
            }
            else
            {
                var model = await _contract.Get(request.Contract.Id);
                if (model == null)
                {
                    return new ServiceMessage()
                    {
                        ErrorId = -1,
                        ErrorTitle = SystemMessages.ContractNotFound,
                        Result = null
                    };
                }
                model.Title = request.Contract.Title;
                model.Content = request.Contract.Content;
                model.UpdatedDateTime = DateTime.UtcNow.AddHours(3.5);
                try
                {
                    await _contract.Update(model);
                    return new ServiceMessage()
                    {
                        ErrorId = 0,
                        ErrorTitle = null,
                        Result = SystemMessages.Success
                    };

                }
                catch (Exception ex)
                {
                    await _errorLog.Add(ex.Message, ex.InnerException?.Message ?? "", "Edit Contract");
                    return new ServiceMessage()
                    {
                        ErrorId = -1,
                        ErrorTitle = SystemMessages.ErrorEditContract,
                        Result = null
                    };
                }
            }
        }
    }
}
