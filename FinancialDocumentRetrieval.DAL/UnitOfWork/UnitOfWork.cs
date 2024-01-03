using FinancialDocumentRetrieval.DAL.Contexts;
using FinancialDocumentRetrieval.DAL.Repositories.Implementation;
using FinancialDocumentRetrieval.DAL.Repositories.Interface;
using FinancialDocumentRetrieval.Models.Entity;

namespace FinancialDocumentRetrieval.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IBaseRepository<TEntity>)_repositories[typeof(TEntity)];
            }

            var repository = new BaseRepository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
