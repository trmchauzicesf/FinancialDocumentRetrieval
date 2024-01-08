using FinancialDocumentRetrieval.Models.Entity;

namespace FinancialDocumentRetrieval.DAL.Repositories.Interface
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Task<string> GetVat(Guid id);
        Task<Client> GetAdditionalClientInfoForVat(string clientVat);
    }
}


