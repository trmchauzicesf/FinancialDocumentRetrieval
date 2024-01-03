using FinancialDocumentRetrieval.BL.Interface;
using FinancialDocumentRetrieval.DAL.Repositories.Interface;
using FinancialDocumentRetrieval.DAL.UnitOfWork;
using FinancialDocumentRetrieval.Models.Entity;
using Microsoft.Extensions.Logging;

namespace FinancialDocumentRetrieval.BL.Implementation
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Client> _clientRepository;
        private readonly ILogger<Client> _logger;
        public ClientService(IUnitOfWork unitOfWork, ILogger<Client> logger)
        {
            _unitOfWork = unitOfWork;
            _clientRepository = _unitOfWork.GetRepository<Client>();
            _logger = logger;
        }

        public async Task<List<Client>> Get()
        {
            return await _clientRepository.GetAllAsync();
        }
    }
}

