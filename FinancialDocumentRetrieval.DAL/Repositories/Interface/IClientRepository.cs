using FinancialDocumentRetrieval.Models.Entity;

namespace FinancialDocumentRetrieval.DAL.Repositories.Interface
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Task<string> GetVatAsync(Guid id);
        Task<Client> GetAdditionalClientInfoForVatAsync(string clientVat);
    }
}


