using FinancialDocumentRetrieval.Models.Entity;

namespace FinancialDocumentRetrieval.BL.Interface
{
    public interface IClientService
    {
        Task<string> GetVatForId(Guid id);
        Task<Client> GetAdditionalClientInfoForVat(string clientVat);
    }
}
