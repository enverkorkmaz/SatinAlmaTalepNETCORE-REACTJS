using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatinAlmaTalep.Entity.DTOs.Product
{
    public class ProductSafeDeleteDto
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; } = true;
    }
}
