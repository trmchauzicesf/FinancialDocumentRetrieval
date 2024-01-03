using FinancialDocumentRetrieval.DAL.Repositories.Interface;
using FinancialDocumentRetrieval.Models.Entity;

namespace FinancialDocumentRetrieval.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }
}
