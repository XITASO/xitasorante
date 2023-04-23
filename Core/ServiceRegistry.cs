using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceRegistry
{
    public static IServiceCollection AddXitasoRanteCoreDomain(this IServiceCollection services)
    {
        return services;
    }

    public static void AddDummyData(this IServiceProvider services)
    {
        var inventory = services.GetRequiredService<IInventory>();
        var recipeProvider = services.GetRequiredService<IRecipeProvider>();
        foreach (var recipe in CreateRecipes())
        {
            recipeProvider.Add(recipe);
            foreach (var ingredient in recipe.Ingredients)
            {
                inventory.RegisterIngredient(ingredient);
            }
        }
    }

    private static IList<Recipe> CreateRecipes()
    {
        return new List<Recipe>
        {
            new(new Dish("Pizza Margeritha", 10.42M, "with cheese"), new[]
            {
                new Ingredient("flour", Unit.Grams, 100),
                new Ingredient("tomato", Unit.Pieces, 1),
                new Ingredient("cheese", Unit.Grams, 75),
            }, "Bake for 1 minute in the oven"),
            new(new Dish("Spaghetti Carbonara", 9.89M, "home made pasta with eggs, hard cheese and cured pork"), new[]
                {
                    new Ingredient("pasta", Unit.Grams, 200),
                    new Ingredient("pancetta", Unit.Grams, 100),
                    new Ingredient("Parmesan cheese", Unit.Grams, 50),
                    new Ingredient("eggs", Unit.Pieces, 2),
                },
                "Boil the pasta until al dente. In a separate pan, cook the pancetta and garlic until crispy. Beat the eggs and Parmesan cheese in a bowl. Drain the pasta and add it to the pan with the pancetta. Turn off the heat and add the egg and cheese mixture. Stir quickly until the sauce thickens. Serve hot."),
            new(new Dish("Spaghetti alla Puttanesca", 8.99M, "with olives, capers, and anchovies"), new[]
                {
                    new Ingredient("spaghetti", Unit.Grams, 200),
                    new Ingredient("tomatoes", Unit.Packs, 1),
                    new Ingredient("olives", Unit.Grams, 50),
                    new Ingredient("capers", Unit.Grams, 25),
                    new Ingredient("anchovy fillets", Unit.Pieces, 4),
                    new Ingredient("red pepper flakes", Unit.Pieces, 20)
                },
                "Boil the spaghetti until al dente. In a pan, heat olive oil and add the anchovy fillets and garlic. Cook until the anchovies dissolve. Add the tomatoes, olives, capers, and red pepper flakes. Simmer until the sauce thickens. Drain the spaghetti and toss it with the sauce. Serve hot."),
            new(new Dish("Tiramisu", 12.99M, "with coffee and mascarpone cheese"), new[]
                {
                    new Ingredient("ladyfingers", Unit.Pieces, 12),
                    new Ingredient("mascarpone cheese", Unit.Grams, 250),
                    new Ingredient("heavy cream", Unit.Liters, 250),
                    new Ingredient("sugar", Unit.Grams, 50),
                    new Ingredient("espresso", Unit.Liters, 100),
                    new Ingredient("cocoa powder", Unit.Grams, 10)
                },
                "In a bowl, beat the mascarpone cheese, heavy cream, and sugar until fluffy. Dip the ladyfingers in the espresso and place them in a dish. Spread half of the mascarpone mixture on top. Repeat with another layer of ladyfingers and mascarpone. Sprinkle cocoa powder on top. Refrigerate for at least 2 hours before serving."),
        };
    }
}