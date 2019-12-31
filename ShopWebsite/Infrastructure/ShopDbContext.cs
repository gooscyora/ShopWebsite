using Microsoft.EntityFrameworkCore;
using ShopWebsite.Models;

namespace ShopWebsite.Infrastructure
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {

        }

        public DbSet<Page> Pages { get; set; }
    }
}
