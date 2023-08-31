using Core;
using Infrastructure.Persistence;

namespace Test.Unit.Core;

public class OrderProcessorTests
{
    [Fact]
    public void It_removes_ingredients_from_inventory_for_two_pizzas()
    {
        var inventory = new InMemoryInventory();
        inventory.RegisterIngredient(new Ingredient("Flour", global::Core.Unit.Grams, 10_000));
        inventory.RegisterIngredient(new Ingredient("Salt", global::Core.Unit.Grams, 5_000));
        inventory.RegisterIngredient(new Ingredient("Tomato", global::Core.Unit.Pieces, 50));
        inventory.RegisterIngredient(new Ingredient("Cheese", global::Core.Unit.Grams, 10_000));
        inventory.RegisterIngredient(new Ingredient("Salami", global::Core.Unit.Grams, 10_000));
        inventory.RegisterIngredient(new Ingredient("Chilli", global::Core.Unit.Pieces, 100));
        inventory.RegisterIngredient(new Ingredient("Water", global::Core.Unit.Liters, 10_000));
        inventory.RegisterIngredient(new Ingredient("Yeast", global::Core.Unit.Pieces, 50));

        var pizzaDiavola = new Dish("Pizza Diavola", 9.5m, "Hot and spicy");
        var pizzaDiavolaRecipe = new Recipe(pizzaDiavola, new[]
            {
                new Ingredient("Flour", global::Core.Unit.Grams, 500),
                new Ingredient("Salt", global::Core.Unit.Grams, 5),
                new Ingredient("Tomato", global::Core.Unit.Pieces, 2),
                new Ingredient("Cheese", global::Core.Unit.Grams, 150),
                new Ingredient("Salami", global::Core.Unit.Grams, 150),
                new Ingredient("Chilli", global::Core.Unit.Pieces, 5),
                new Ingredient("Water", global::Core.Unit.Liters, 0.25),
                new Ingredient("Yeast", global::Core.Unit.Pieces, 0.5),
            },
            """
1.    Sieve the flour/s and salt on to a clean work surface and make a well in the middle.
2.    In a jug, mix the yeast, sugar and oil into 650ml of lukewarm water and leave for a few minutes, then pour into the well.
3.    Using a fork, bring the flour in gradually from the sides and swirl it into the liquid. Keep mixing, drawing larger amounts of flour in, and when it all starts to come together, work the rest of the flour in with your clean, flour-dusted hands. Knead until you have a smooth, springy dough.
4.    Place the ball of dough in a large flour-dusted bowl and flour the top of it. Cover the bowl with a damp cloth and place in a warm room for about an hour until the dough has doubled in size.
5.    Now remove the dough to a flour-dusted surface and knead it around a bit to push the air out with your hands – this is called knocking back the dough. You can either use it immediately, or keep it, wrapped in clingfilm, in the fridge (or freezer) until required.
6.    If using straight away, divide the dough up into as many little balls as you want to make pizzas – this amount of dough is enough to make about six to eight medium pizzas.
7.    Timing-wise, it’s a good idea to roll the pizzas out about 15 to 20 minutes before you want to cook them. Don’t roll them out and leave them hanging around for a few hours, though – if you are working in advance like this it’s better to leave your dough, covered with clingfilm, in the fridge. However, if you want to get them rolled out so there’s one less thing to do when your guests are round, simply roll the dough out into rough circles, about 0.5cm thick, and place them on slightly larger pieces of olive-oil-rubbed and flour-dusted tin foil. You can then stack the pizzas, cover them with clingfilm, and pop them into the fridge.
8.    Add Salami, Cheese and Chillies.
9.    Put it into the oven a few minutes.
""");
        var order = new Order();
        order.AddDish(pizzaDiavola);
        order.AddDish(pizzaDiavola);

        var repository = new InMemoryOrderManagement(new CalculationService());
        var recipes = new InMemoryRecipeStore();
        recipes.Add(pizzaDiavolaRecipe);

        var orderProcessor = new OrderProcessor(inventory, recipes, repository);
        orderProcessor.Process(order);

        var ingredientAmounts = inventory.Ingredients.ToDictionary(i => i.Name, i => i.Amount);
        ingredientAmounts["Flour"].Should().Be(9_000);
        ingredientAmounts["Salt"].Should().Be(4_990);
        ingredientAmounts["Tomato"].Should().Be(46);
        ingredientAmounts["Cheese"].Should().Be(9_700);
        ingredientAmounts["Salami"].Should().Be(9_700);
        ingredientAmounts["Chilli"].Should().Be(90);
        ingredientAmounts["Water"].Should().Be(9_999.5);
        ingredientAmounts["Yeast"].Should().Be(49);
    }
    
    
    [Fact]
    public void It_does_not_remove_ingredients_if_it_cannot_process_the_order()
    {
        var inventory = new InMemoryInventory();
        inventory.RegisterIngredient(new Ingredient("Flour", global::Core.Unit.Grams, 10_000));
        inventory.RegisterIngredient(new Ingredient("Salt", global::Core.Unit.Grams, 5_000));
        inventory.RegisterIngredient(new Ingredient("Tomato", global::Core.Unit.Pieces, 50));
        inventory.RegisterIngredient(new Ingredient("Cheese", global::Core.Unit.Grams, 10_000));
        inventory.RegisterIngredient(new Ingredient("Salami", global::Core.Unit.Grams, 10_000));
        inventory.RegisterIngredient(new Ingredient("Chilli", global::Core.Unit.Pieces, 0));
        inventory.RegisterIngredient(new Ingredient("Water", global::Core.Unit.Liters, 10_000));
        inventory.RegisterIngredient(new Ingredient("Yeast", global::Core.Unit.Pieces, 50));

        var pizzaDiavola = new Dish("Pizza Diavola", 9.5m, "Hot and spicy");
        var pizzaDiavolaRecipe = new Recipe(pizzaDiavola, new[]
            {
                new Ingredient("Flour", global::Core.Unit.Grams, 500),
                new Ingredient("Salt", global::Core.Unit.Grams, 5),
                new Ingredient("Tomato", global::Core.Unit.Pieces, 2),
                new Ingredient("Cheese", global::Core.Unit.Grams, 150),
                new Ingredient("Salami", global::Core.Unit.Grams, 150),
                new Ingredient("Chilli", global::Core.Unit.Pieces, 5),
                new Ingredient("Water", global::Core.Unit.Liters, 0.25),
                new Ingredient("Yeast", global::Core.Unit.Pieces, 0.5),
            },
            """
1.    Sieve the flour/s and salt on to a clean work surface and make a well in the middle.
2.    In a jug, mix the yeast, sugar and oil into 650ml of lukewarm water and leave for a few minutes, then pour into the well.
3.    Using a fork, bring the flour in gradually from the sides and swirl it into the liquid. Keep mixing, drawing larger amounts of flour in, and when it all starts to come together, work the rest of the flour in with your clean, flour-dusted hands. Knead until you have a smooth, springy dough.
4.    Place the ball of dough in a large flour-dusted bowl and flour the top of it. Cover the bowl with a damp cloth and place in a warm room for about an hour until the dough has doubled in size.
5.    Now remove the dough to a flour-dusted surface and knead it around a bit to push the air out with your hands – this is called knocking back the dough. You can either use it immediately, or keep it, wrapped in clingfilm, in the fridge (or freezer) until required.
6.    If using straight away, divide the dough up into as many little balls as you want to make pizzas – this amount of dough is enough to make about six to eight medium pizzas.
7.    Timing-wise, it’s a good idea to roll the pizzas out about 15 to 20 minutes before you want to cook them. Don’t roll them out and leave them hanging around for a few hours, though – if you are working in advance like this it’s better to leave your dough, covered with clingfilm, in the fridge. However, if you want to get them rolled out so there’s one less thing to do when your guests are round, simply roll the dough out into rough circles, about 0.5cm thick, and place them on slightly larger pieces of olive-oil-rubbed and flour-dusted tin foil. You can then stack the pizzas, cover them with clingfilm, and pop them into the fridge.
8.    Add Salami, Cheese and Chillies.
9.    Put it into the oven a few minutes.
""");
        var order = new Order();
        order.AddDish(pizzaDiavola);

        var recipes = new InMemoryRecipeStore();
        var repository = new InMemoryOrderManagement(new CalculationService());
        recipes.Add(pizzaDiavolaRecipe);

        var orderProcessor = new OrderProcessor(inventory, recipes, repository);
        var doProcess = () => orderProcessor.Process(order);

        doProcess.Should().Throw<IngredientExhausted>();

        var ingredientAmounts = inventory.Ingredients.ToDictionary(i => i.Name, i => i.Amount);
        ingredientAmounts["Flour"].Should().Be(10_000);
        ingredientAmounts["Salt"].Should().Be(5_000);
        ingredientAmounts["Tomato"].Should().Be(50);
        ingredientAmounts["Cheese"].Should().Be(10_000);
        ingredientAmounts["Salami"].Should().Be(10_000);
        ingredientAmounts["Chilli"].Should().Be(0);
        ingredientAmounts["Water"].Should().Be(10_000);
        ingredientAmounts["Yeast"].Should().Be(50);
    }
}