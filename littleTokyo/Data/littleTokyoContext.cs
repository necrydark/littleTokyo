using Microsoft.AspNetCore.Identity;
using littleTokyo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace littleTokyo.Data;

public class littleTokyoContext : IdentityDbContext<ApplicationUser>
{
    public littleTokyoContext(DbContextOptions<littleTokyoContext> options)
        : base(options)
    {
    }

    public DbSet<Menu> Menus { get; set; }
    public DbSet<CheckoutCustomer> checkoutCustomers { get; set; } = default;
    public DbSet<Basket> baskets { get; set; } = default;

    public DbSet<BasketItem> BasketItems { get; set; } = default;

    public DbSet<OrderHistory> OrderHistories { get; set; } = default;

    public DbSet<OrderItem> OrderItems { get; set; } = default;

    [NotMapped]
    public DbSet<CheckoutItem> CheckoutItems { get; set; } = default;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Menu>().ToTable("Menu");
        builder.Entity<BasketItem>().HasKey(t => new { t.StockID, t.BasketID });
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);

    }
}