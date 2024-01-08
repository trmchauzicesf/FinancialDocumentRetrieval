using FinancialDocumentRetrieval.BL.Implementation.Strategy;
using FinancialDocumentRetrieval.BL.Interface.Factory;
using FinancialDocumentRetrieval.BL.Interface.Strategy;
using FinancialDocumentRetrieval.Models.Common.Enums;

namespace FinancialDocumentRetrieval.BL.Implementation.Factory
{
    public class ValidationFactory : IValidationFactory
    {
        public IValidationStrategy Create(AppEnums.EntityName entityName)
        {
            if (string.IsNullOrEmpty(nameof(entityName)))
            {
                throw new ArgumentNullException();
            }
            switch (entityName)
            {
                case AppEnums.EntityName.Product:
                    return new ProductStrategy();

                case AppEnums.EntityName.Tenant:
                    return new TenantStrategy();

                default:
                    return new TenantClientStrategy();
            }
        }
    }
}
