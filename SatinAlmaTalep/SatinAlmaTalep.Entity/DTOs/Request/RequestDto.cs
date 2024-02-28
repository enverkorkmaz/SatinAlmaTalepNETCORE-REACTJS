using SatinAlmaTalep.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatinAlmaTalep.Entity.DTOs.Request
{
    public class RequestDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Urgency { get; set; }
    }
}
