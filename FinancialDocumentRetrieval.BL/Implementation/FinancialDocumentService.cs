﻿using AutoMapper;
using FinancialDocumentRetrieval.BL.Interface;
using FinancialDocumentRetrieval.BL.Interface.Factory;
using FinancialDocumentRetrieval.DAL.UnitOfWork;
using FinancialDocumentRetrieval.Models.Common;
using FinancialDocumentRetrieval.Models.Common.Enums;
using FinancialDocumentRetrieval.Models.DTOs.RequestDTOs;
using FinancialDocumentRetrieval.Models.DTOs.ResponseDTOs;
using FinancialDocumentRetrieval.Models.Entity;
using Microsoft.Extensions.Logging;

namespace FinancialDocumentRetrieval.BL.Implementation
{
    public class FinancialDocumentService : IFinancialDocumentService
    {
        private readonly IRepositoryInitUnitOfWork _unitOfWork;
        private readonly IValidationFactory _validationFactory;
        private readonly IClientService _clientService;
        private readonly IAnonymizationService _anonymizationService;
        private readonly ILogger<FinancialDocument> _logger;
        private readonly IMapper _mapper;

        public FinancialDocumentService(IRepositoryInitUnitOfWork unitOfWork, ILogger<FinancialDocument> logger,
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

        public async Task<FinancialDocumentResponseDto> Get(FinancialDocumentRequestDTO financialDocumentRequestDto)
        {
            var financialDocumentValidation = _mapper.Map<FinancialDocumentValidation>(financialDocumentRequestDto);

            await Validate(financialDocumentValidation, AppEnums.EntityName.Product);
            await Validate(financialDocumentValidation, AppEnums.EntityName.Tenant);

            var clientId = await _unitOfWork.FinancialDocumentRepository
                .GetClientIdForTenantIdAndDocumentId(financialDocumentRequestDto.TenantId,
                    financialDocumentRequestDto.DocumentId);
            financialDocumentValidation.ClientId = clientId;

            await Validate(financialDocumentValidation, AppEnums.EntityName.ClientTenant);
            var clientVat = await _clientService.GetVatForId(clientId);

            var additionalClientInfo = await _clientService.GetAdditionalClientInfoForVat(clientVat);

            var documentResponseDto = new FinancialDocumentResponseDto
            {
                Company = _mapper.Map<CompanyDTO>(additionalClientInfo)
            };

            var documentData = await _unitOfWork.FinancialDocumentRepository
                .GetDataForTenantIdAndDocumentId(financialDocumentRequestDto.TenantId,
                    financialDocumentRequestDto.DocumentId);

            documentResponseDto.Data =
                _anonymizationService.AnonymizeFinancialDocument(documentData,
                    financialDocumentRequestDto.ProductCode);

            return documentResponseDto;
        }

        #region Private

        private async Task Validate(FinancialDocumentValidation financialDocumentValidation,
            AppEnums.EntityName entityName)
        {
            var strategy = _validationFactory.Create(entityName);

            await strategy.Validate(financialDocumentValidation, _unitOfWork);
        }

        #endregion
    }
}