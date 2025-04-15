using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.DbContexts
{
    public class StoreDbContext : DbContext
    {

        // CLR Will inject DbContext
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyRefferances).Assembly);   // better way : AssemblyRefferances is a class i created in Infrastructure\Persistence\AssemblyRefferances.cs from it we can access the project assembly
        }
    }
}
