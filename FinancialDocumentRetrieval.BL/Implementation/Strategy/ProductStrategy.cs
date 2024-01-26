using FinancialDocumentRetrieval.BL.Interface.Strategy;
using FinancialDocumentRetrieval.DAL.UnitOfWork;
using FinancialDocumentRetrieval.Models.Common.Exceptions;
using FinancialDocumentRetrieval.Models.Common.Validation;
using FinancialDocumentRetrieval.Models.Entity;
using System.Linq.Expressions;

namespace FinancialDocumentRetrieval.BL.Implementation.Strategy
{
    public class ProductStrategy : IValidationStrategy
    {
        public async Task ValidateAsync(FinancialDocumentValidation financialDocumentValidation, IRepositoryInitUnitOfWork unitOfWork)
        {
            Expression<Func<Product, bool>> predicate = p => p.Code == financialDocumentValidation.ProductCode && p.IsActive;
            var isProductCodeActive = await unitOfWork.ProductRepository.CheckIfExistsAsync(predicate);
            if (!isProductCodeActive)
            {
                throw new FinancialDocumentRetrievalException("Product code is not supported");
            }
        }
    }
}