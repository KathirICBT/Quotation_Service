using Microsoft.EntityFrameworkCore;
using Supplier_A.Model;

namespace Supplier_A.OrderService.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<OrderModel> Orders { get; set; }
    }
}
