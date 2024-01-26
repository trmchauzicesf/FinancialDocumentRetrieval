using FinancialDocumentRetrieval.Models.Entity;

namespace FinancialDocumentRetrieval.DAL.Repositories.Interface
{
    public interface IFinancialDocumentRepository : IBaseRepository<FinancialDocument>
    {
        Task<Guid> GetClientIdForTenantIdAndDocumentIdAsync(Guid tenantId, Guid documentId);
        Task<string> GetDataForTenantIdAndDocumentIdAsync(Guid tenantId, Guid documentId);
    }
}


