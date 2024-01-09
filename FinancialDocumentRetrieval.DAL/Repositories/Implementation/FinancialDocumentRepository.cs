using FinancialDocumentRetrieval.DAL.Repositories.Interface;
using FinancialDocumentRetrieval.Models.Common.Exceptions;
using FinancialDocumentRetrieval.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace FinancialDocumentRetrieval.DAL.Repositories.Implementation
{
    public class FinancialDocumentRepository : BaseRepository<FinancialDocument>, IFinancialDocumentRepository
    {
        public async Task<Guid> GetClientIdForTenantIdAndDocumentId(Guid tenantId, Guid documentId)
        {
            return await Context.FinancialDocuments
                .Where(fd => fd.TenantId == tenantId && fd.Id == documentId)
                .Select(fd => fd.ClientId)
                .FirstOrDefaultAsync();
        }

        public async Task<string> GetDataForTenantIdAndDocumentId(Guid tenantId, Guid documentId)
        {
            var data = await Context.FinancialDocuments
                .Where(fd => fd.TenantId == tenantId && fd.Id == documentId)
                .Select(fd => fd.Data)
                .FirstOrDefaultAsync();

            if (data == null)
            {
                throw new NotFoundException(nameof(GetDataForTenantIdAndDocumentId), tenantId);
            }

            return data;
        }
    }
}