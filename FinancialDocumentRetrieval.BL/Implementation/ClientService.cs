using FinancialDocumentRetrieval.BL.Interface;
using FinancialDocumentRetrieval.DAL.UnitOfWork;
using FinancialDocumentRetrieval.Models.Common.Enums;
using FinancialDocumentRetrieval.Models.Common.Exceptions;
using FinancialDocumentRetrieval.Models.Entity;
using Microsoft.Extensions.Logging;

namespace FinancialDocumentRetrieval.BL.Implementation
{
    public class ClientService : IClientService
    {
        private readonly IRepositoryInitUnitOfWork _unitOfWork;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IRepositoryInitUnitOfWork unitOfWork, ILogger<ClientService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<string> GetVatForId(Guid id)
        {
            return await _unitOfWork.ClientRepository.GetVat(id);
        }

        public async Task<Client> GetAdditionalClientInfoForVat(string clientVat)
        {
            var additionalClientInfo = await _unitOfWork.ClientRepository.GetAdditionalClientInfoForVat(clientVat);
            if (additionalClientInfo != null && additionalClientInfo.CompanyType == nameof(AppEnums.CompanyType.small))
            {
                throw new FinancialDocumentRetrievalException($"Company Type is {nameof(AppEnums.CompanyType.small)}");
            }

            return additionalClientInfo;
        }
    }
}