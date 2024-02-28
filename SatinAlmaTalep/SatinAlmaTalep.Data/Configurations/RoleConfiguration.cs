using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using SatinAlmaTalep.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatinAlmaTalep.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //MIGRATIONA EKLE
            //migrationBuilder.InsertData(
            //   table: "AspNetUserRoles",
            //   columns: new[] { "UserId", "RoleId" },
            //   values: new object[,]
            //   {
            //        { new Guid("1cc852e7-5cfb-4bbf-923f-202c31e6364a"), new Guid("f019fcdd-6cf0-404f-9cf5-0b20ed168dd2") }, // Personel kullanıcısına Personel rolü
            //        { new Guid("25d61a8a-083b-4220-8965-2302dc958e7e"), new Guid("f26e3bad-5cbc-478b-bda5-7eaba68e24e0") }, // Satın Alma Müdürü kullanıcısına Satın Alma Müdürü rolü
            //        { new Guid("92af874a-41fa-407f-9c81-261218e4e30f"), new Guid("c3675ed3-0d29-4331-8d71-c85ba496c5e1") }  // Personel Amiri kullanıcısına Personel Amiri rolü
            //   });

            var roles = new Role[]
            {
                new Role
                {
                    Id = Guid.Parse("F019FCDD-6CF0-404F-9CF5-0B20ED168DD2"),
                    Name = "personel",
                    NormalizedName = "PERSONEL"
                },
                new Role
                {
                    Id = Guid.Parse("C3675ED3-0D29-4331-8D71-C85BA496C5E1"),
                    Name = "personelamiri",
                    NormalizedName = "PERSONELAMIRI"
                },
                new Role
                {
                    Id = Guid.Parse("F26E3BAD-5CBC-478B-BDA5-7EABA68E24E0"),
                    Name = "satinalmamuduru",
                    NormalizedName = "SATINALMAMUDURU"
                }
            };

            builder.HasData(roles);
        }
    }
}
