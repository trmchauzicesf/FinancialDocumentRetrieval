using FinancialDocumentRetrieval.DAL.Contexts;
using FinancialDocumentRetrieval.DAL.Repositories.Interface;
using FinancialDocumentRetrieval.Models.Common.Exceptions;
using FinancialDocumentRetrieval.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace FinancialDocumentRetrieval.DAL.Repositories.Implementation
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DatabaseContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(DatabaseContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

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

        public async Task<TEntity> GetAsync(int? id)
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


        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();

            return entity;
        }
    }
}
