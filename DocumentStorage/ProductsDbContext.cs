using DocumentStorage.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentStorage;

public class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}
