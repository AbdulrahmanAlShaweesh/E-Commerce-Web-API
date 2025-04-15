using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;

namespace Persistence
{
    // CLR : Inject DbContext
    public class DataSeeding(StoreDbContext _dbContex) : IDataSeeding
    {
        public async Task DataSeedAsync()  // this function must be exit before the application start
        {
            try
            {
                // Seed data [Occurs only one eith in production or deploying]
                var PendingMigration = await _dbContex.Database.GetPendingMigrationsAsync();
                if ((PendingMigration).Any())
                {
                   await _dbContex.Database.MigrateAsync();  // apply all migrations that are not applied to databasse (Pending Migrations)
                }
                
                // check if  brand table does not have a data
                if (!_dbContex.ProductBrands.Any()) // adding instaed or writng whole path becouse this couse be change
                {
                    //var ProductBrandData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeedData\brands.json"); // to read json and return task of string
                    var ProductBrandData =  File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeedData\brands.json"); // to read json and return file stream

                    // convert into c# object [BrandProduct]
                    // if we have string and we need to convert it into obbject we need to disserilze
                    var ProductBrand = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);

                    if (ProductBrand is not null && ProductBrand.Any()){
                        await _dbContex.ProductBrands.AddRangeAsync(ProductBrand);
                    }

                }

                if (!_dbContex.ProductTypes.Any())
                {
                    var ProductTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeedData\types.json");

                    var ProductType = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);

                    if (ProductType is not null && ProductType.Any()) await _dbContex.ProductTypes.AddRangeAsync(ProductType);
                }

                if (!_dbContex.Products.Any())
                {
                    var ProductData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeedData\products.json");
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);

                    if (Products is not null && Products.Any()) await _dbContex.Products.AddRangeAsync(Products);
                }

               await _dbContex.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // To do
            }

        }
    }
}
