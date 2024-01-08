using FinancialDocumentRetrieval.BL.Interface.Strategy;
using FinancialDocumentRetrieval.Models.Common.Enums;

namespace FinancialDocumentRetrieval.BL.Interface.Factory
{
    public interface IValidationFactory
    {
        public IValidationStrategy Create(AppEnums.EntityName entityName);
    }
}
