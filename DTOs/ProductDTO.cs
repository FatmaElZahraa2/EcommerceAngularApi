using Microsoft.AspNetCore.Http;

namespace EcommerceAngularProject.DTOs
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public IFormFile ? Image { get; set; }
        public int CateogryId { get; set; }
    }
}
