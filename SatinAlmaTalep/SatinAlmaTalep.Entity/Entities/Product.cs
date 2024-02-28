using SatinAlmaTalep.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatinAlmaTalep.Entity.Entities
{
    public class Product : EntityBase,IEntityBase
    {
        public Product()
        {
            
        }
        public Product(string name, decimal price)
        {
            Name= name;
            Price= price;

        }
        public string Name { get; set; }
        public decimal Price { get; set; }
        //public ICollection<Request> Requests { get; set; }
    }
}
