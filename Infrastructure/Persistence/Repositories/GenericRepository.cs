
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;

namespace Persistence.Repositories
{
    // any one will use Generic Repo, need provide both TEntity and TKey
    // Note: if are going to work with unit of work pattern (where need to have more than one repo] then we can savechanges
    public class GenericRepository<TEntity, TKey>(StoreDbContext dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        public async Task AddAsync(TEntity entity) => await dbContext.Set<TEntity>().AddAsync(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await dbContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByIdSync(TKey id) => await dbContext.Set<TEntity>().FindAsync(id);

        public void Remove(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);  

        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);
    }
}
