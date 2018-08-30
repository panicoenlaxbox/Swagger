using Microsoft.EntityFrameworkCore;

namespace ApiNSwag
{
    public class ShopContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }
    }
}