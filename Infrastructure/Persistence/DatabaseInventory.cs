using Core;

namespace Infrastructure.Persistence;

public class DatabaseInventory: IInventory
{
    private readonly XitasoranteDBContext db;

    public class DBIngredient
    {
        public Guid Id { get; private set; }
        public string Name { get; set; } = null!;
        public double Amount { get; set; }
        public Unit Unit { get; set; }
    }

    public DatabaseInventory(XitasoranteDBContext db)
    {
        this.db = db;
    }

    public IEnumerable<Ingredient> Ingredients
    {
        get
        {
            return db.Ingredients.Select(i => new Ingredient(i.Name, i.Unit, i.Amount));
        }
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