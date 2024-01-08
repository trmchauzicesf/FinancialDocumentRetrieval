using FinancialDocumentRetrieval.BL.Interface.Strategy;
using FinancialDocumentRetrieval.DAL.UnitOfWork;
using FinancialDocumentRetrieval.Models.Common;
using FinancialDocumentRetrieval.Models.Common.Exceptions;
using FinancialDocumentRetrieval.Models.DTOs;
using FinancialDocumentRetrieval.Models.Entity;
using System.Linq.Expressions;

namespace FinancialDocumentRetrieval.BL.Implementation.Strategy
{
    public class TenantClientStrategy : IValidationStrategy
    {
        public async Task Validate(FinancialDocumentValidation financialDocumentValidation, IRepositoryInitUnitOfWork unitOfWork)
        {
            Expression<Func<TenantClient, bool>> predicate = tc => tc.TenantId == financialDocumentValidation.TenantId
            && tc.ClientId == financialDocumentValidation.ClientId && tc.IsActive;

            var isTenantClientCodeActive = await unitOfWork.TenantClientRepository.CheckIfExistsAsync(predicate);

            if (!isTenantClientCodeActive)
            {
                throw new FinancialDocumentRetrievalException("Client is not whitelisted");
            }
        }
    }
}