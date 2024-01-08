using FinancialDocumentRetrieval.DAL.Repositories.Interface;

namespace FinancialDocumentRetrieval.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
