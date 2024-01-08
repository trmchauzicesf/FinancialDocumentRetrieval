using FinancialDocumentRetrieval.DAL.UnitOfWork;
using FinancialDocumentRetrieval.Models.Common;

namespace FinancialDocumentRetrieval.BL.Interface.Strategy
{
    public interface IValidationStrategy
    {
        Task Validate(FinancialDocumentValidation financialDocumentValidation, IRepositoryInitUnitOfWork unitOfWork);
    }
}
