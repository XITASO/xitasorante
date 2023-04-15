using System.Collections.Immutable;
using Core;

namespace Test.Core;

public class IngredientTests
{
    [Fact]
    public void Has_correct_initialValues()
    {
        var ingredient = new Ingredient("Tomato", Unit.Pieces, 50);

        ingredient.Name.Should().Be("Tomato");
        ingredient.Unit.Should().Be(Unit.Pieces);
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

internal interface ITestDatabuilder<T>
{
    T Build();
}

internal class IngredientBuilder: ITestDatabuilder<Ingredient>
{
    private uint amount = 50;
    private string name = "Olives";
    private Unit unit = Unit.Packs;

    public IngredientBuilder WithName(string value)
    {
        name = value;
        return this;
    }

    public IngredientBuilder WithAmount(uint value)
    {
        amount = value;
        return this;
    }

    public IngredientBuilder WithUnit(Unit value)
    {
        unit = value;
        return this;
    }
    
    public Ingredient Build()
    {
        return new Ingredient(name, unit, amount);
    }
}