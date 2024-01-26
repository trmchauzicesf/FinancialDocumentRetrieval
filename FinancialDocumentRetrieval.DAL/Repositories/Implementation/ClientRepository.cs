using FinancialDocumentRetrieval.DAL.Contexts;
using FinancialDocumentRetrieval.DAL.Repositories.Interface;
using FinancialDocumentRetrieval.Models.Common.Exceptions;
using FinancialDocumentRetrieval.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace FinancialDocumentRetrieval.DAL.Repositories.Implementation
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public async Task<string> GetVatAsync(Guid id)
        {
            return await Context.Clients
                .Where(c => c.Id == id)
                .Select(c => c.Vat)
                .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(GetVatAsync), id);
        }

        public async Task<Client> GetAdditionalClientInfoForVatAsync(string clientVat)
        {
            return await Context.Clients
                       .Where(c => c.Vat == clientVat)
                       .Select(c => new Client
                       {
                           RegistrationNumber = c.RegistrationNumber,
                           CompanyType = c.CompanyType
                       }).FirstOrDefaultAsync() ??
                   throw new NotFoundException(nameof(GetAdditionalClientInfoForVatAsync), clientVat);
        }
    }
}