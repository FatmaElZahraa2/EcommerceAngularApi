using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceAngularProject.Models;
using EcommerceAngularProject.Repository;
using EcommerceAngularProject.DTOs;
using System.IO;

namespace EcommerceAngularProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository= productRepository;
        }


        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetById(id);

            return product == null ? NotFound() : Ok(product);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,[FromForm] ProductDTO productDTO)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                return NotFound();
            if (productDTO.Image != null)
            {
                using var DataStream = new MemoryStream();

                await productDTO.Image.CopyToAsync(DataStream);
                product.Image = DataStream.ToArray();
            }
            product.Name = productDTO.Name;
            product.Quantity = productDTO.Quantity;
            product.Price = productDTO.Price;
            


            _productRepository.Update(product);
            return Ok(product);
        }


        [HttpPost]

        public async Task<ActionResult> PostProduct([FromForm] ProductDTO productDTO)
        {
           if(productDTO.Image==null)
                return BadRequest("Image is Required !");
         
            using var DataStream = new MemoryStream();

            await productDTO.Image.CopyToAsync(DataStream);

            var product = new Product()
            {
                Name = productDTO.Name,
                Quantity = productDTO.Quantity,
                Price=productDTO.Price,
                Image=DataStream.ToArray(),
                CateogryId = productDTO.CateogryId,
            };
            await _productRepository.Insert(product);
            return Ok(product);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                return NotFound();

            _productRepository.Delete(product);
            return Ok(product);
        }


    }
}
