using FinancialDocumentRetrieval.DAL.Contexts;
using FinancialDocumentRetrieval.DAL.Repositories.Interface;
using FinancialDocumentRetrieval.Models.Common.Exceptions;
using FinancialDocumentRetrieval.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace FinancialDocumentRetrieval.DAL.Repositories.Implementation
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public async Task<string> GetVat(Guid id)
        {
            return await Context.Clients
                .Where(c => c.Id == id)
                .Select(c => c.Vat)
                .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(GetVat), id);
        }

        public async Task<Client> GetAdditionalClientInfoForVat(string clientVat)
        {
            return await Context.Clients
                       .Where(c => c.Vat == clientVat)
                       .Select(c => new Client
                       {
                           RegistrationNumber = c.RegistrationNumber,
                           CompanyType = c.CompanyType
                       }).FirstOrDefaultAsync() ??
                   throw new NotFoundException(nameof(GetAdditionalClientInfoForVat), clientVat);
        }
    }
}