using Core;
using Test.Unit.TestDataBuilders;

namespace Test.Common.TestDataBuilders;

public class IngredientBuilder: ITestDataBuilder<Ingredient>
{
    private uint amount = 50;
    private string name = "Olives";
    private global::Core.Unit unit = global::Core.Unit.Packs;

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

    public IngredientBuilder WithUnit(global::Core.Unit value)
    {
        unit = value;
        return this;
    }
    
    public Ingredient Build()
    {
        return new Ingredient(name, unit, amount);
    }
}