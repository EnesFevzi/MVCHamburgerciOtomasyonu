using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using System.Reflection;

namespace MVCHamburgerciOtomasyonu.DataAccess.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<Menu> Menus { get; set; }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<MenuSize> MenuSizes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
