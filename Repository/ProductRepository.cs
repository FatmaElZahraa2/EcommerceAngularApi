using EcommerceAngularProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceAngularProject.Repository
{
    public class ProductRepository : IProductRepository { 

       private readonly Context _context;
        public ProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> Insert(Product item)
        {
            await _context.AddAsync(item);
            _context.SaveChanges();
            return item;
        }

        public Product Update(Product item)
        {
            _context.Update(item);
            _context.SaveChanges();
            return item;
        }
        public Product Delete(Product item)
        {
            _context.Remove(item);
            _context.SaveChanges();
            return item;
        }
    }
}
