using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }  // navigation prob : mandatory

        public int TypeId { get; set; }
        public ProductType ProductType { get; set; }

    }
}
