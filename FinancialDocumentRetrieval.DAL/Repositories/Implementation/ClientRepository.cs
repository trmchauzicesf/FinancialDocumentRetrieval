using FinancialDocumentRetrieval.DAL.Contexts;
using FinancialDocumentRetrieval.DAL.Repositories.Interface;
using FinancialDocumentRetrieval.Models.Entity;

namespace FinancialDocumentRetrieval.DAL.Repositories.Implementation
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(DatabaseContext context) : base(context) { }
    }
}
