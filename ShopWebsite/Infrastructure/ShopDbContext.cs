﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopWebsite.Models;

namespace ShopWebsite.Infrastructure
{
    public class ShopDbContext : IdentityDbContext<AppUser>
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {

        }
        public DbSet<Page> Pages { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}
