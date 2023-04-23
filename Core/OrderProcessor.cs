namespace Core;

public class OrderProcessor : IOrderProcessor
{
    private readonly IInventory inventory;
    private readonly IRecipeProvider recipes;
    private readonly IOrderRepository orderRepository;

    public OrderProcessor(IInventory inventory, IRecipeProvider recipes, IOrderRepository orderRepository)
    {
        this.inventory = inventory;
        this.recipes = recipes;
        this.orderRepository = orderRepository;
    }

    public void Process(Order order)
    {
        var requiredRecipes = order.Positions
            .SelectMany(position => Enumerable.Repeat(recipes.Get(position.Dish), (int)position.Amount));


        var processedIngredients = new List<Ingredient>();
        try
        {
            foreach (var currentRecipe in requiredRecipes)
            {
                foreach (var requiredIngredient in currentRecipe.Ingredients)
                {
                    inventory.GetByName(requiredIngredient.Name).DecreaseAmount(requiredIngredient.Amount);
                    processedIngredients.Add(requiredIngredient);
                }
            }
        }
        catch (IngredientExhausted)
        {
            processedIngredients.ForEach(i => inventory.GetByName(i.Name).IncreaseAmount(i.Amount));
            throw;
        }
        orderRepository.DispatchOrder(order);
    }
}