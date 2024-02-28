using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatinAlmaTalep.Core.Entites
{
    public abstract class EntityBase : IEntityBase
    {
        public virtual int Id { get; set; }
        public virtual string? CreatedBy { get; set; } = "envermain";
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual bool isDeleted { get; set; } = false;


    }
}
