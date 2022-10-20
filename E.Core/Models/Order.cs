using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.Core.Models
{
    public class Order:BaseEntity
    {
        public string Description { get; set; }

        public string Address { get; set; }

        public ICollection<Product> Products { get; set; }

        public int ProductId { get; set; }

        public Customer Customer { get; set; }
    }
}
