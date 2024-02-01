using AutoMapper;
using FinancialDocumentRetrieval.BL.Implementation.Strategy;
using FinancialDocumentRetrieval.BL.Interface;
using FinancialDocumentRetrieval.BL.Interface.Factory;
using FinancialDocumentRetrieval.DAL.UnitOfWork;
using FinancialDocumentRetrieval.Models.Common.Enums;
using FinancialDocumentRetrieval.Models.Common.Validation;
using FinancialDocumentRetrieval.Models.DTOs.RequestDTOs;
using FinancialDocumentRetrieval.Models.DTOs.ResponseDTOs;
using Microsoft.Extensions.Logging;

namespace FinancialDocumentRetrieval.BL.Implementation
{
    public class FinancialDocumentService : IFinancialDocumentService
    {
        private readonly IRepositoryInitUnitOfWork _unitOfWork;
        private readonly IValidationFactory _validationFactory;
        private readonly IClientService _clientService;
        private readonly IAnonymizationService _anonymizationService;
        private readonly ILogger<FinancialDocumentService> _logger;
        private readonly IMapper _mapper;

        public FinancialDocumentService(IRepositoryInitUnitOfWork unitOfWork, ILogger<FinancialDocumentService> logger,
            IValidationFactory validationFactory, IMapper mapper, IClientService clientService,
            IAnonymizationService anonymizationService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _validationFactory = validationFactory;
            _mapper = mapper;
            _clientService = clientService;
            _anonymizationService = anonymizationService;
        }

        public async Task<FinancialDocumentResponseDto> RetrieveAsync(FinancialDocumentRequestDTO financialDocumentRequestDto)
        {
            var financialDocumentValidation = _mapper.Map<FinancialDocumentValidation>(financialDocumentRequestDto);

            await ValidateAsync(financialDocumentValidation, AppEnums.EntityName.Product);
            await ValidateAsync(financialDocumentValidation, AppEnums.EntityName.Tenant);

            var clientId = await _unitOfWork.FinancialDocumentRepository
                .GetClientIdForTenantIdAndDocumentIdAsync(financialDocumentRequestDto.TenantId,
                    financialDocumentRequestDto.DocumentId);
            financialDocumentValidation.ClientId = clientId;

            await ValidateAsync(financialDocumentValidation, AppEnums.EntityName.ClientTenant);
            var clientVat = await _clientService.GetVatForIdAsync(clientId);

            var additionalClientInfo = await _clientService.GetAdditionalClientInfoForVatAsync(clientVat);

            var documentResponseDto = new FinancialDocumentResponseDto
            {
                Company = _mapper.Map<CompanyDTO>(additionalClientInfo)
            };

            var documentData = await _unitOfWork.FinancialDocumentRepository
                .GetDataForTenantIdAndDocumentIdAsync(financialDocumentRequestDto.TenantId,
                    financialDocumentRequestDto.DocumentId);

            documentResponseDto.Data =
                _anonymizationService.AnonymizeFinancialDocument(documentData,
                    financialDocumentRequestDto.ProductCode);

            return documentResponseDto;
        }

        #region Private

        private async Task ValidateAsync(FinancialDocumentValidation financialDocumentValidation,
            AppEnums.EntityName entityName)
        {
            var strategy = _validationFactory.Create(entityName);

            await strategy.ValidateAsync(financialDocumentValidation, _unitOfWork);
        }

        #endregion
    }
}