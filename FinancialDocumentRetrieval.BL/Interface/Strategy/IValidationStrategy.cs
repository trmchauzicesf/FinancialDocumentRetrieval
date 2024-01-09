using FinancialDocumentRetrieval.DAL.UnitOfWork;
using FinancialDocumentRetrieval.Models.Common.Validation;

namespace FinancialDocumentRetrieval.BL.Interface.Strategy
{
    public interface IValidationStrategy
    {
        Task Validate(FinancialDocumentValidation financialDocumentValidation, IRepositoryInitUnitOfWork unitOfWork);
    }
}
