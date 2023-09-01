using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class XitasoranteDBContext : DbContext
{
    public DbSet<DatabaseInventory.DBIngredient> Ingredients => Set<DatabaseInventory.DBIngredient>();

    public XitasoranteDBContext(DbContextOptions<XitasoranteDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DatabaseInventory.DBIngredient>();
    }
}