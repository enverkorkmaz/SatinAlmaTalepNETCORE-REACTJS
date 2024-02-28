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
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            Request request1 = new()
            {
                Id = 1,
                ProductId=1,
                Quantity =4,
                Status= Entity.Enums.RequestStatus.Pending,
                Urgency="None",
                CreatedBy="enver",
                isDeleted=false,
                CreatedDate=DateTime.Now
                

            };
            Request request2 = new()
            {
                Id = 2,
                ProductId = 2,
                Quantity = 5,
                Status = Entity.Enums.RequestStatus.Pending,
                Urgency = "None",
                CreatedBy = "enver2",
                isDeleted = false,
                CreatedDate = DateTime.Now


            };
            builder.HasData(request1,request2);
        }
    }
}
