using SatinAlmaTalep.Data.UnitOfWorks;
using SatinAlmaTalep.Entity.Entities;
using SatinAlmaTalep.Service.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatinAlmaTalep.Service.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            await unitOfWork.GetRepository<Product>().AddAsync(product);
            await unitOfWork.SaveAsync();
            return product; 
        }

        public async Task<Product> DeleteProductAsync(Product product)
        {
            await unitOfWork.GetRepository<Product>().DeleteAsync(product);
            await unitOfWork.SaveAsync();
            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await unitOfWork.GetRepository<Product>().GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await unitOfWork.GetRepository<Product>().GetByIdAsync(id);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            await unitOfWork.GetRepository<Product>().UpdateAsync(product);
            await unitOfWork.SaveAsync();
            return product;
        }
    }
}
