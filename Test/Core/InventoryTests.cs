using Core;
using Test.TestDataBuilders;

namespace Test.Core;

public class InventoryTests
{
    [Fact]
    public void Ingredients_can_be_registered()
    {
        var inventory = new InMemoryInventory();
        var ingredient = new IngredientBuilder().Build();
        inventory.RegisterIngredient(ingredient);

        inventory.Ingredients.Should().BeEquivalentTo(new[] { ingredient });
    }
    
    [Fact]
    public void Throws_on_ingredient_with_same_name()
    {
        var inventory = new InMemoryInventory();
        var ingredient1 = new IngredientBuilder().WithName("Tomato").Build();
        var ingredient2 = new IngredientBuilder().WithName("Tomato").Build();
        inventory.RegisterIngredient(ingredient1);
        var registerSameIngredient = () => inventory.RegisterIngredient(ingredient2);

        registerSameIngredient.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Contain(ingredient2.Name);
    }
}