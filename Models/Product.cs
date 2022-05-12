using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAngularProject.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }
        [ForeignKey("Category")]
        public int CateogryId { get; set; }

        public virtual Category Category { get; set; }  
    }
}
