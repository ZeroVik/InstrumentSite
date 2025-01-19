using InstrumentSite.Enums;
using InstrumentSite.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;


namespace InstrumentSite.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Define relationship
            // Product to Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order to OrderDetails
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            // OrderDetail to Product
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId);

            // Cart to CartItems
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // CartItem to Product
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Store enums as strings
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            // Unique constraint on Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Seed categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Guitars" },
                new Category { Id = 2, Name = "Drums" }
            );

            // Seed an admin user
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                FirstName = "Alex",
                LastName = "Kacov",
                Email = "admin@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"), // Use a secure library for hashing
                Role = UserRoleEnum.Admin,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Acoustic Guitar", Price = 199.99m, Description = "Guitar", ImageUrl = "uploads/Guitar.jpg", CategoryId = 1 },
            new Product { Id = 2, Name = "Electric Drum Kit", Price = 499.99m, Description = "Electric Drums", ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse4.mm.bing.net%2Fth%3Fid%3DOIP.GDvI_nVc29ofgxfXFao1vwHaHa%26pid%3DApi&f=1&ipt=1fec529b0b28d922c840939fbff720313f7075d9b04d2fb5bf8c45cd07137c17&ipo=images", CategoryId = 2 }
            );
        }
    }
}

