using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product> // Configuration per class
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.ProductBrand)
                   .WithMany().HasForeignKey(P => P.BrandId);   // one to many configration even if we do not add Icollection in many but it still understand it

            builder.HasOne(P => P.ProductType)
                   .WithMany().HasForeignKey(P => P.TypeId);

            builder.Property(P => P.Price).HasColumnType("decimal(10,2)");
        }
    }
}
