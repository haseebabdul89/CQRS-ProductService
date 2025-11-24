using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;

namespace Product.Infra.Persistence
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
        public DbSet<Product.Domain.Entities.Product> Products { get; set; }
    }
}