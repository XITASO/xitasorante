using System.ComponentModel;
using Core;

namespace DomainServices;

public interface IRecipeService
{
    IEnumerable<Recipe> GetRecipes();
    Recipe? GetByName(string name);
    void AddRecipe(RecipeData recipeData);

    public record RecipeData
    {
        public required string Title { get; init; }
        public required string Description { get; init; }
        public required string Instructions { get; init; }
        public required decimal PriceInEuro { get; init; }

        public required IEnumerable<string> Ingredients { get; init; }
    }

}

public class RecipeService : IRecipeService
{
    private readonly IMenu menu;
    private readonly IRecipeProvider recipeProvider;
    private readonly IInventory inventory;

    public RecipeService(IMenu menu, IRecipeProvider recipeProvider, IInventory inventory)
    {
        this.menu = menu;
        this.recipeProvider = recipeProvider;
        this.inventory = inventory;
    }
    public IEnumerable<Recipe> GetRecipes()
    {
        return menu.GetMenu().Select(dish => recipeProvider.Get(dish));
    }

    public Recipe? GetByName(string name)
    {
        var dish = menu.GetMenu().SingleOrDefault(m => m.Title == name);
        return dish == null ? null : recipeProvider.Get(dish);
    }

    public void AddRecipe(IRecipeService.RecipeData recipeData)
    {
        var dish = new Dish(recipeData.Title, recipeData.PriceInEuro, recipeData.Description);
        var ingredients = recipeData.Ingredients
            .Select(ingredientName => inventory.GetByName(ingredientName));
        var recipe = new Recipe(dish, ingredients, recipeData.Instructions);
        recipeProvider.Add(recipe);
    }
}