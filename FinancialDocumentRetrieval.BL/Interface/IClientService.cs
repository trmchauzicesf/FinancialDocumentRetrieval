using FinancialDocumentRetrieval.Models.Entity;

namespace FinancialDocumentRetrieval.BL.Interface
{
    public interface IClientService
    {
        public Task<List<Client>> Get();
    }
}
