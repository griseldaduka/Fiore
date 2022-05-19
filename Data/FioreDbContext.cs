using Fiore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiore.Data;

public class FioreDbContext : IdentityDbContext<ApplicationUser>
{
    public FioreDbContext(DbContextOptions<FioreDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ProductOrderDetails> ProductOrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ProductOrderDetails>()
            .HasKey(o => new { o.OrderId, o.ProductId });

        builder.Entity<Order>()
            .HasOne(u => u.ApplicationUser)
            .WithMany(o => o.Orders)
            .HasForeignKey(o => o.UserId)
            .HasPrincipalKey(u => u.Id);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguraation());

    }
}

public class ApplicationUserEntityConfiguraation : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(128);
        builder.Property(u => u.LastName).HasMaxLength(128);
    }
}