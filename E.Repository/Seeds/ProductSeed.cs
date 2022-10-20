using E.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "kalem1",
                Price = 100,
                Stock = 20,
                CreatedDate = DateTime.Now
            },
           new Product
           {
               Id = 2,
               CategoryId = 1,
               Name = "kalem2",
               Price = 200,
               Stock = 30,
               CreatedDate = DateTime.Now
           });
          
        }
    }
}
