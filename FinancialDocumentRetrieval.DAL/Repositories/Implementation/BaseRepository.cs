using FinancialDocumentRetrieval.DAL.Contexts;
using FinancialDocumentRetrieval.DAL.Repositories.Interface;
using FinancialDocumentRetrieval.Models.Common.Exceptions;
using FinancialDocumentRetrieval.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinancialDocumentRetrieval.DAL.Repositories.Implementation
{
    public abstract class BaseRepository
    {
        protected DatabaseContext Context { get; private set; }

        public virtual void SetDbContext(DatabaseContext context)
        {
            this.Context = context;
        }
    }

    public class BaseRepository<TEntity> : BaseRepository, IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbSet<TEntity> DbSet => Context.Set<TEntity>();

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntity = (await DbSet.AddAsync(entity)).Entity;
            await Context.SaveChangesAsync();

            return addedEntity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            var removedEntity = DbSet.Remove(entity).Entity;
            await Context.SaveChangesAsync();

            return removedEntity;
        }

        public async Task<TEntity> GetAsync(Guid? id)
        {
            var result = await DbSet.FindAsync(id);
            if (result is null)
            {
                throw new NotFoundException(typeof(TEntity).Name, id.HasValue ? id : "No Key Provided");
            }

            return result;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<bool> CheckIfExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AnyAsync(predicate);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();

            return entity;
        }
    }
}