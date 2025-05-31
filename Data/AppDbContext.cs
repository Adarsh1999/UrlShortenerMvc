using Microsoft.EntityFrameworkCore;
using UrlShortenerMvc.Models;

namespace UrlShortenerMvc.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

    public DbSet<ShortUrl> ShortUrls => Set<ShortUrl>();
    public DbSet<Click> Clicks => Set<Click>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<ShortUrl>()
          .HasIndex(u => u.ShortCode)
          .IsUnique();

        // Cascade delete clicks when a ShortUrl is removed
        b.Entity<Click>()
          .HasOne(c => c.ShortUrl)
          .WithMany(u => u.Clicks)
          .HasForeignKey(c => c.ShortUrlId);
    }
}