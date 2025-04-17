using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Persistence.Data.DbContexts;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
           // Get Type Name
           var typeName = typeof(TEntity).Name;

            // Dic<String, Object> ==> string key [Name of Type] -- Object an object from generic repository
            //if (_repositories.ContainsKey(typeName)) 
            //    return (IGenericRepository<TEntity, TKey>) _repositories[typeName];
            if (_repositories.TryGetValue(typeName, out object? value))
                return (IGenericRepository<TEntity, TKey>)value;

            else
            {
                // Create object
                var repo = new GenericRepository<TEntity, TKey>(_dbContext);
                // Store object into dic
                _repositories["typeName"] = repo;
                // Return the object
                return repo;
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();  // save changes
 
    }
}
