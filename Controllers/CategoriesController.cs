using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcommerceAngularProject.Repository;
using Microsoft.EntityFrameworkCore;
using EcommerceAngularProject.DTOs;
using EcommerceAngularProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceAngularProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository= categoryRepository;
        }

       
        [HttpGet]
      //  [Authorize]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetAll();
            return Ok(categories);
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategory(int id)
        {
            var category = await _categoryRepository.GetById(id);    

           return category== null ? NotFound() : Ok(category); 
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,[FromBody] CategoryDTO categoryDTO)
        {
            var category = await _categoryRepository.GetById(id);
            if(category == null)
                return NotFound();

            category.Name = categoryDTO.Name;
            _categoryRepository.Update(category);
            return Ok(category);
        }

       
        [HttpPost]
        public async Task<ActionResult> InsertCategory(CategoryDTO categoryDTO)
        {
            var category = new Category() { Name = categoryDTO.Name };
            await _categoryRepository.Insert(category);

            return Ok(category);
          
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetById(id);

            if (category == null)
                return NotFound();

            _categoryRepository.Delete(category);
            return Ok(category);
        }

      
    }
}
