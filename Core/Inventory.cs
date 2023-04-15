namespace Core;

public class Inventory
{
    private readonly Dictionary<string, Ingredient> ingredients = new Dictionary<string, Ingredient>();

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