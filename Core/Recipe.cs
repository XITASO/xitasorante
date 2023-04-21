namespace Core;

public class Recipe
{
    public Dish Dish { get; }
    public IEnumerable<Ingredient> Ingredients { get; }
    public string Instructions { get; }

    public Recipe(Dish dish, IEnumerable<Ingredient> ingredients, string instructions)
    {
        Dish = dish;
        Ingredients = ingredients;
        Instructions = instructions;
    }
}