using Microsoft.AspNetCore.Identity;
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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        public void Configure(EntityTypeBuilder<User> builder)
        {

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            var users = new User[]
            {
                new User
                {
                    Id = Guid.Parse("1CC852E7-5CFB-4BBF-923F-202C31E6364A"),
                    UserName = "personel@gmail.com",
                    Email = "personel@gmail.com",
                    FullName = "Personel",
                    RefreshToken = null,
                    RefreshTokenExpiryTime = null,
                    PasswordHash = _passwordHasher.HashPassword(null, "12345personel."),
                    NormalizedEmail = "PERSONEL@GMAIL.COM"
                },
                 new User
                {
                    Id = Guid.Parse("92AF874A-41FA-407F-9C81-261218E4E30F"),
                    UserName = "personelamiri@gmail.com",
                    Email = "personelamiri@gmail.com",
                    FullName = "Personel Amiri",
                    RefreshToken = null,
                    RefreshTokenExpiryTime = null,
                    PasswordHash = _passwordHasher.HashPassword(null, "12345personelamiri."),
                    NormalizedEmail = "PERSONELAMIRI@GMAIL.COM"
                },
                  new User
                {
                    Id = Guid.Parse("25D61A8A-083B-4220-8965-2302DC958E7E"),
                    UserName = "satinalmamuduru@gmail.com",
                    Email = "satinalmamuduru@gmail.com",
                    FullName = "satinalmamuduru",
                    RefreshToken = null,
                    RefreshTokenExpiryTime = null,
                    PasswordHash = _passwordHasher.HashPassword(null, "12345satinalmamuduru."),
                    NormalizedEmail = "SATINALMAMUDURU@GMAIL.COM"

                },
            };

            builder.HasData(users);
        }
    }
}

