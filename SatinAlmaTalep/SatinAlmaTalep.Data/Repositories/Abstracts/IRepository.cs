using SatinAlmaTalep.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatinAlmaTalep.Data.Repositories.Abstracts
{
    public interface IRepository<T> where T : class, IEntityBase,new() 
    {
        Task AddAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);

    }
}
