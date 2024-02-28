using Microsoft.EntityFrameworkCore;
using SatinAlmaTalep.Core.Entites;
using SatinAlmaTalep.Data.Context;
using SatinAlmaTalep.Data.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatinAlmaTalep.Data.Repositories.Concretes
{
    public class Repository<T>: IRepository<T> where T : class,IEntityBase,new()
    {
        private readonly AppDbContext dbContext;

        public Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private DbSet<T> Table { get => dbContext.Set<T>(); }
        public async Task<List<T>> GetAllAsync()
        {
            IQueryable<T> query = Table;
            return await query.ToListAsync();
        }
        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task<T> GetAsync()
        {
            IQueryable<T> query = Table;
            return await query.SingleAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => Table.Update(entity));
            return entity;

        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => Table.Remove(entity));
        }
    }
}
