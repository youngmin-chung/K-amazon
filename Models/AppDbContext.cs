using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using K_amazon.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace K_amazon.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderLineItem> OrderLineItems { get; set; }
        //public virtual DbSet<Branch> Branches { get; set; }
    }
}
