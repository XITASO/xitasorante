namespace Core;

public class Inventory
{
    public Inventory()
    {
        RegisterIngredient(new Ingredient("Tomato", Unit.Pieces, 30));
        RegisterIngredient(new Ingredient("Olive Oil", Unit.Liters, 20));
        RegisterIngredient(new Ingredient("Flour", Unit.Grams, 50_000));
            
    }
    private readonly Dictionary<string, Ingredient> ingredients = new();

    public IEnumerable<Ingredient> Ingredients => ingredients.Values;

    public void RegisterIngredient(Ingredient ingredient)
    {
        if (ingredients.ContainsKey(ingredient.Name))
        {
            throw new InvalidOperationException($"Ingredient with name '{ingredient.Name}' already exists");
        }
        ingredients.Add(ingredient.Name, ingredient);
    }

}