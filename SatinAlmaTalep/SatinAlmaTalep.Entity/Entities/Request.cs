using SatinAlmaTalep.Core.Entites;
using SatinAlmaTalep.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatinAlmaTalep.Entity.Entities
{
    public class Request : EntityBase,IEntityBase
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string? Urgency { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public decimal? TotalPrice { get; set; }

    }
}
