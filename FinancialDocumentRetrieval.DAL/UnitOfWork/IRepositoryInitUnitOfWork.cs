using FinancialDocumentRetrieval.DAL.Repositories.Interface;

namespace FinancialDocumentRetrieval.DAL.UnitOfWork
{
    public interface IRepositoryInitUnitOfWork : IUnitOfWork
    {
        public IFinancialDocumentRepository FinancialDocumentRepository { get; }
        public IClientRepository ClientRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ITenantRepository TenantRepository { get; }
        public ITenantClientRepository TenantClientRepository { get; }
    }
}