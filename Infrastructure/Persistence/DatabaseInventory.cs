using Core;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence;

public class DatabaseInventory : IInventory
{
    private readonly XitasoranteDBContext db;

    public class DBIngredient
    {
        public Guid Id { get; private set; }
        public string Name { get; set; } = null!;
        public double Amount { get; set; }
        public Unit Unit { get; set; }
    }

    public DatabaseInventory(XitasoranteDBContext db, InfraStructureConfig config)
    {
        this.db = db;
        if (config.AddDummyData && !db.Ingredients.Any())
        {
            AddDummyData();
        }
    }

    private void AddDummyData()
    {
        db.Ingredients.AddRange(
                new DBIngredient
                {
                    Name = "flour", Unit = Unit.Grams,
                    Amount = 100d
                },
                new DBIngredient
                {
                    Name = "tomato", Unit = Unit.Pieces,
                    Amount = 1d
                },
                new DBIngredient
                {
                    Name = "cheese", Unit = Unit.Grams,
                    Amount = 75d
                },
                new DBIngredient
                {
                    Name = "pasta", Unit = Unit.Grams,
                    Amount = 200d
                },
                new DBIngredient
                {
                    Name = "pancetta", Unit = Unit.Grams,
                    Amount = 100d
                },
                new DBIngredient
                {
                    Name = "Parmesan cheese", Unit = Unit.Grams,
                    Amount = 50d
                },
                new DBIngredient
                {
                    Name = "eggs", Unit = Unit.Pieces,
                    Amount = 2d
                },
                new DBIngredient
                {
                    Name = "spaghetti", Unit = Unit.Grams,
                    Amount = 200d
                },
                new DBIngredient
                {
                    Name = "tomatoes", Unit = Unit.Packs,
                    Amount = 1d
                },
                new DBIngredient
                {
                    Name = "olives", Unit = Unit.Grams,
                    Amount = 50d
                },
                new DBIngredient
                {
                    Name = "capers", Unit = Unit.Grams,
                    Amount = 25d
                },
                new DBIngredient
                {
                    Name = "anchovy fillets", Unit = Unit.Pieces,
                    Amount = 4d
                },
                new DBIngredient
                {
                    Name = "red pepper flakes",
                    Unit = Unit.Pieces, Amount = 20d
                },
                new DBIngredient
                {
                    Name = "ladyfingers", Unit = Unit.Pieces,
                    Amount = 12d
                },
                new DBIngredient
                {
                    Name = "mascarpone cheese",
                    Unit = Unit.Grams, Amount = 250d
                },
                new DBIngredient
                {
                    Name = "heavy cream", Unit = Unit.Liters,
                    Amount = 250d
                },
                new DBIngredient
                {
                    Name = "sugar", Unit = Unit.Grams,
                    Amount = 50d
                },
                new DBIngredient
                {
                    Name = "espresso", Unit = Unit.Liters,
                    Amount = 100d
                },
                new DBIngredient
                {
                    Name = "cocoa powder", Unit = Unit.Grams,
                    Amount = 10d
                });
    }

    public IEnumerable<Ingredient> Ingredients
    {
        get { return db.Ingredients.Select(i => new Ingredient(i.Name, i.Unit, i.Amount)); }
    }

    public void RegisterIngredient(Ingredient ingredient)
    {
        db.Ingredients.Add(new DBIngredient
        {
            Name = ingredient.Name,
            Amount = ingredient.Amount,
            Unit = ingredient.Unit
        });
        db.SaveChanges();
    }

    public Ingredient GetByName(string ingredientName)
    {
        var result = db.Ingredients.SingleOrDefault(i => i.Name == ingredientName);
        if (result == null)
        {
            throw new KeyNotFoundException($"Ingredient '{ingredientName}' not found");
        }

        return new Ingredient(result.Name, result.Unit, result.Amount);
    }
}