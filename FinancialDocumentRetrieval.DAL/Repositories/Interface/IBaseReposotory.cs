using FinancialDocumentRetrieval.Models.Entity;
using System.Linq.Expressions;

namespace FinancialDocumentRetrieval.DAL.Repositories.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetAsync(Guid? id);

        Task<List<TEntity>> GetAllAsync();

        Task<bool> CheckIfExistsAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);
    }
}