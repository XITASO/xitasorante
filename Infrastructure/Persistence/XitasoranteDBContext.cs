using Core;
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
        modelBuilder.Entity<DatabaseInventory.DBIngredient>()
            .HasData(
                new  { Id = new Guid("3F29B668-20BF-4836-B4B9-7F38C0AB4EE7"), Name = "flour", Unit = Unit.Grams, Amount = 100d },
                new  { Id = new Guid("9B2507C0-78FC-4571-8728-78C4557F41D2"), Name = "tomato", Unit = Unit.Pieces, Amount = 1d },
                new  { Id = new Guid("DDD74AE5-1345-495F-85CE-50E3F9733770"), Name = "cheese", Unit = Unit.Grams, Amount = 75d },
                new  { Id = new Guid("3D39B5BC-8B25-4BCF-97EB-B7C7071EC457"), Name = "pasta", Unit = Unit.Grams, Amount = 200d },
                new  { Id = new Guid("6CD66F6D-4899-45E4-9A31-C68FDB2C4DFA"), Name = "pancetta", Unit = Unit.Grams, Amount = 100d },
                new  { Id = new Guid("42AC06DD-ADE7-49E4-B5C3-7C6DF9CA394E"), Name = "Parmesan cheese", Unit = Unit.Grams, Amount = 50d },
                new  { Id = new Guid("D1B73503-1BD8-48E4-945C-4D6806C8565A"), Name = "eggs", Unit = Unit.Pieces, Amount = 2d },
                new  { Id = new Guid("87D3A2B4-67AB-4773-8301-9C9CF88DAA96"), Name = "spaghetti", Unit = Unit.Grams, Amount = 200d },
                new  { Id = new Guid("842C6C80-2799-4E36-BEF7-7E7F72FC3121"), Name = "tomatoes", Unit = Unit.Packs, Amount = 1d },
                new  { Id = new Guid("9DA8F35A-E6B3-4C30-B1E5-82C73820FAE3"), Name = "olives", Unit = Unit.Grams, Amount = 50d },
                new  { Id = new Guid("5B97465E-C4D5-4A10-8AF4-8872D8C8984C"), Name = "capers", Unit = Unit.Grams, Amount = 25d },
                new  { Id = new Guid("5E8D07CF-B63B-4DE9-BBF2-FF9BF628D44B"), Name = "anchovy fillets", Unit = Unit.Pieces, Amount = 4d },
                new  { Id = new Guid("0C257FE9-6A05-4180-8CBD-A0B2FB7762B5"), Name = "red pepper flakes", Unit = Unit.Pieces, Amount = 20d },
                new  { Id = new Guid("222F5DC0-FE84-44FA-A28B-9A374CBF95CB"), Name = "ladyfingers", Unit = Unit.Pieces, Amount = 12d },
                new  { Id = new Guid("5C52832A-D88C-4DE0-A3AF-B844E3183221"), Name = "mascarpone cheese", Unit = Unit.Grams, Amount = 250d },
                new  { Id = new Guid("71AD94E1-161D-42EA-8EBE-AB36A9F0B12B"), Name = "heavy cream", Unit = Unit.Liters, Amount = 250d },
                new  { Id = new Guid("01DE28C6-C36B-4AF4-827C-0A24D9E06A6B"), Name = "sugar", Unit = Unit.Grams, Amount = 50d },
                new  { Id = new Guid("BC0A5D53-5B84-49CB-B7F0-89D03C0C6589"), Name = "espresso", Unit = Unit.Liters, Amount = 100d },
                new  { Id = new Guid("F8829D14-56FB-4D45-9B9D-87E1CBDCA91D"), Name = "cocoa powder", Unit = Unit.Grams, Amount = 10d });
    }
}