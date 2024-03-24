using Microsoft.EntityFrameworkCore;
using StockControlAPI.Models.Entities;

namespace StockControlAPI.Models
{
    public class StockApiDBContext : DbContext
    {

        public StockApiDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
             new Category() { Id = 1, CategoryName = "Bilgisayar" },
             new Category() { Id = 2, CategoryName = "Beyaz Eşya" }
            );

            modelBuilder.Entity<Warehouse>().HasData(
             new Warehouse() { Id = 1, WarehouseName = "Ankara" },
             new Warehouse() { Id = 2, WarehouseName = "İstanbul" },
             new Warehouse() { Id = 3, WarehouseName = "İzmir" }
            );

            modelBuilder.Entity<Product>().HasData(
                 new Product() { Id = 1, CategoryId = 1, WarehouseId = 1, ProductName = "Casper", Count = 3, Price = 1000},
                 new Product() { Id = 2, CategoryId = 1, WarehouseId = 2, ProductName = "Lenovo", Count = 5, Price = 1500 },
                 new Product() { Id = 3, CategoryId = 2, WarehouseId = 2, ProductName = "Buzdolabı", Count = 4, Price = 3000 },
                 new Product() { Id = 4, CategoryId = 2, WarehouseId = 3, ProductName = "Fırın", Count = 6, Price = 2000 }
            );

            modelBuilder.Entity<User>().HasData(
                 new User() { Id = 1, UserName = "elif", Password = "elif1" },
                 new User() { Id = 2, UserName = "ege", Password = "ege1" }
            );
        }
    }
}
