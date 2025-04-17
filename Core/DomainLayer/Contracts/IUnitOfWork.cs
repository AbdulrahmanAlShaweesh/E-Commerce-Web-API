using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        // we need to have a signture of pro for each rep
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>; // we will use it to registor the repo, every time we create a repo from the unit of work, this function should be called
        Task<int> SaveChangesAsync();  // step 01
    }
}
