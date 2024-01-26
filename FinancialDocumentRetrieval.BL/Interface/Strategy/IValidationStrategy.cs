using FinancialDocumentRetrieval.DAL.UnitOfWork;
using FinancialDocumentRetrieval.Models.Common.Validation;

namespace FinancialDocumentRetrieval.BL.Interface.Strategy
{
    public interface IValidationStrategy
    {
        Task ValidateAsync(FinancialDocumentValidation financialDocumentValidation, IRepositoryInitUnitOfWork unitOfWork);
    }
}
