using backend.Models;
using Microsoft.EntityFrameworkCore;

public class EcosystemDbContext : DbContext
{
    public EcosystemDbContext(DbContextOptions<EcosystemDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ecosimulator.db");
    }
}
