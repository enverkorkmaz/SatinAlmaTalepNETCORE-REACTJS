using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatinAlmaTalep.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatinAlmaTalep.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(256);

            Product product1 = new()
            {
                Id = 1,
                Name = "Product1",
                CreatedDate = DateTime.Now,
                CreatedBy = "denemeenver",
                Price=500,
                isDeleted = false
                
            };
            Product product2 = new()
            {
                Id = 2,
                Name = "Product2",
                CreatedDate = DateTime.Now,
                CreatedBy = "denemeenver2",
                Price = 600,
                isDeleted = false

            };
            builder.HasData(product1,product2);
        }
    }
}
