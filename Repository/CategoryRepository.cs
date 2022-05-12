using EcommerceAngularProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceAngularProject.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;
        public CategoryRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> Insert(Category item)
        {
            await _context.AddAsync(item);
            _context.SaveChanges();
            return item;
        }

        public Category Update(Category item)
        {
            _context.Update(item);
            _context.SaveChanges();
            return item;
        }
        public Category Delete(Category item)
        {
            _context.Remove(item);
            _context.SaveChanges();
            return item;
        }

       
    }
}
