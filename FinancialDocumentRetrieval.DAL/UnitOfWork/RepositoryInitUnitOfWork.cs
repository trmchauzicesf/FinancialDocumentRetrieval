using FinancialDocumentRetrieval.DAL.Contexts;
using FinancialDocumentRetrieval.DAL.Repositories.Implementation;
using FinancialDocumentRetrieval.DAL.Repositories.Interface;

namespace FinancialDocumentRetrieval.DAL.UnitOfWork
{
    public class RepositoryInitUnitOfWork : UnitOfWork, IRepositoryInitUnitOfWork
    {
        public RepositoryInitUnitOfWork(DatabaseContext context)
            : base(context)
        { }

        public IFinancialDocumentRepository FinancialDocumentRepository =>
            GetOrInitRepository<FinancialDocumentRepository>();

        public IClientRepository ClientRepository =>
            GetOrInitRepository<ClientRepository>();

        public IProductRepository ProductRepository =>
            GetOrInitRepository<ProductRepository>();

        public ITenantRepository TenantRepository =>
            GetOrInitRepository<TenantRepository>();

        public ITenantClientRepository TenantClientRepository =>
            GetOrInitRepository<TenantClientRepository>();
    }
}


