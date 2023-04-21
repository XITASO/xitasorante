namespace Core;

public class InMemoryInventory : IInventory
{
    public InMemoryInventory()
    {
            
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

    public Ingredient GetByName(string ingredientName)
    {
        return ingredients[ingredientName];
    }
}