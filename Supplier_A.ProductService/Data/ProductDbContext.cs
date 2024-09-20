using Microsoft.EntityFrameworkCore;
using Supplier_A.Model;

namespace Supplier_A.ProductService.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }        

        public DbSet<ProductModel> Products { get; set; }
    }
}
