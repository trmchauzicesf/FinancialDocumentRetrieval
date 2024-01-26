using FinancialDocumentRetrieval.Models.Entity;

namespace FinancialDocumentRetrieval.BL.Interface
{
    public interface IClientService
    {
        Task<string> GetVatForIdAsync(Guid id);
        Task<Client> GetAdditionalClientInfoForVatAsync(string clientVat);
    }
}
