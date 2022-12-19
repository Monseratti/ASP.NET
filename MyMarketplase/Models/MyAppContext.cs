using Microsoft.EntityFrameworkCore;

namespace MyMarketplase.Models
{
    public class MyAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Nomenclature> Nomenclatures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FilesPath> FilesPaths { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<WarehouseNomenclature> WarehouseNoms { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderState> OrderStates { get; set; }
        public DbSet<OrderNomenclature> SellingNoms { get; set; }

        public MyAppContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
