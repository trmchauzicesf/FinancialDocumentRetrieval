using FinancialDocumentRetrieval.Models.Entity;

namespace FinancialDocumentRetrieval.DAL.Repositories.Interface
{
    public interface IFinancialDocumentRepository : IBaseRepository<FinancialDocument>
    {
        Task<Guid> GetClientIdForTenantIdAndDocumentId(Guid tenantId, Guid documentId);
        Task<string> GetDataForTenantIdAndDocumentId(Guid tenantId, Guid documentId);
    }
}


