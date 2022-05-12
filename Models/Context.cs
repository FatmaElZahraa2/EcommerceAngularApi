using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAngularProject.Models
{
    public class Context:IdentityDbContext<ApplicationUser>
    {
        public Context()
        {
                
        }
        public Context(DbContextOptions<Context> options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
