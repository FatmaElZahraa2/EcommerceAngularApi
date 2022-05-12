using EcommerceAngularProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceAngularProject.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<Product> Insert(Product item);
        Product Update(Product item);
        Product Delete(Product item);
    }
}
