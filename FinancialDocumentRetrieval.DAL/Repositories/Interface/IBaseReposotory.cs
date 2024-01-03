using FinancialDocumentRetrieval.Models.Entity;

namespace FinancialDocumentRetrieval.DAL.Repositories.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetAsync(int? id);

        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);
    }
}
