using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.Core.Models
{
    public class ProductFeature
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Color { get; set; }

        public string? Material { get; set; }

        public int Size { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public string? Description { get; set; }

        
    }
}
