using System.Collections.Immutable;
using Core;
using Test.Unit.TestDataBuilders;

namespace Test.Unit.Core;

public class IngredientTests
{
    [Fact]
    public void Has_correct_initialValues()
    {
        var ingredient = new Ingredient("Tomato", global::Core.Unit.Pieces, 50);

        ingredient.Name.Should().Be("Tomato");
        ingredient.Unit.Should().Be(global::Core.Unit.Pieces);
        ingredient.Amount.Should().Be(50);
    }

    [Fact]
    public void Amount_can_be_increased()
    {
        var ingredient = new IngredientBuilder().WithAmount(20).Build();
        ingredient.IncreaseAmount(30);

        ingredient.Amount.Should().Be(50);
    }
    
    [Fact]
    public void Amount_can_be_decreased()
    {
        var ingredient = new IngredientBuilder().WithAmount(50).Build();
        ingredient.DecreaseAmount(30);

        ingredient.Amount.Should().Be(20);
    }
    
    [Fact]
    public void Throws_if_decreased_more_units_than_available()
    {
        var ingredient = new IngredientBuilder().WithAmount(10).Build();
        var decreaseTooMuch = () =>ingredient.DecreaseAmount(30);

        decreaseTooMuch.Should().Throw<IngredientExhausted>().And.Message.Should().Contain(ingredient.Name);
    }
}