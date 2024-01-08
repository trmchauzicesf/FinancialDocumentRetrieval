using FinancialDocumentRetrieval.BL.Interface.Strategy;
using FinancialDocumentRetrieval.DAL.UnitOfWork;
using FinancialDocumentRetrieval.Models.Common;
using FinancialDocumentRetrieval.Models.Common.Exceptions;
using FinancialDocumentRetrieval.Models.Entity;
using System.Linq.Expressions;

namespace FinancialDocumentRetrieval.BL.Implementation.Strategy
{
    public class TenantStrategy : IValidationStrategy
    {
        public async Task Validate(FinancialDocumentValidation financialDocumentValidation, IRepositoryInitUnitOfWork unitOfWork)
        {
            Expression<Func<Tenant, bool>> predicate = p => p.Id == financialDocumentValidation.TenantId && p.IsActive;

            var isTenantCodeActive = await unitOfWork.TenantRepository.CheckIfExistsAsync(predicate);

            if (!isTenantCodeActive)
            {
                throw new FinancialDocumentRetrievalException("Tenant is not whitelisted");
            }
        }
    }
}