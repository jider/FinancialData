using Microsoft.EntityFrameworkCore;
using findata_api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace findata_api.Data;

public class ApplicationDBContext(DbContextOptions dbContextOptions) : IdentityDbContext<AppUser>(dbContextOptions)
{
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Portfolio setup
        builder.Entity<Portfolio>(portfolio => portfolio.HasKey(p => new { p.AppUserId, p.StockId }));

        builder.Entity<Portfolio>()
            .HasOne(p => p.AppUser)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.AppUserId);

        builder.Entity<Portfolio>()
            .HasOne(p => p.Stock)
            .WithMany(s => s.Portfolios)
            .HasForeignKey(p => p.StockId);


        // Seeds
        List<IdentityRole> roles = [
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            }
        ];

        builder.Entity<IdentityRole>().HasData(roles);
    }
}


