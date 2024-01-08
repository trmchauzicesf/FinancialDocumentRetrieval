using FinancialDocumentRetrieval.DAL.Contexts;
using FinancialDocumentRetrieval.DAL.Repositories.Implementation;

namespace FinancialDocumentRetrieval.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private readonly object _repositoryInitLockObj = new();
        private Dictionary<Type, BaseRepository> _repositoryInstances = new();

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        protected TRepository GetOrInitRepository<TRepository>() where TRepository : BaseRepository, new()
        {
            return GetOrInitRepository(() => new TRepository());
        }

        protected TRepository GetOrInitRepository<TRepository>(Func<TRepository> repositoryCreator) where TRepository : BaseRepository
        {

            lock (_repositoryInitLockObj)
            {
                if (!_repositoryInstances.ContainsKey(typeof(TRepository)))
                {
                    var repository = repositoryCreator();
                    repository.SetDbContext(_context);

                    _repositoryInstances[typeof(TRepository)] = repository;
                }

                return (TRepository)_repositoryInstances[typeof(TRepository)];
            }
        }
    }
}
