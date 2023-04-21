using Core;

namespace Test.TestDataBuilders;

internal class IngredientBuilder: ITestDataBuilder<Ingredient>
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