using EcommerceAngularProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceAngularProject.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int id);
        Task<Category> Insert(Category item);
        Category Update(Category item);
        Category Delete (Category item);

    }
}
