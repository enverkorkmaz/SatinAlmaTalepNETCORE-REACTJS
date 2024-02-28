using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SatinAlmaTalep.Data.Context;
using SatinAlmaTalep.Data.Repositories.Abstracts;
using SatinAlmaTalep.Data.Repositories.Concretes;
using SatinAlmaTalep.Data.UnitOfWorks;
using SatinAlmaTalep.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatinAlmaTalep.Data
{
    public static class Registration
    {
        public static void AddDataReg(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 2;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit= false;
                opt.SignIn.RequireConfirmedEmail = false;

            }).AddRoles<Role>().AddEntityFrameworkStores<AppDbContext>();

            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
