namespace Core;

public class Ingredient
{
    public string Name { get; }
    public Unit Unit { get; }

    public double Amount { get; private set; }

    public Ingredient(string name, Unit unit, double initialAmount = 0)
    {
        Name = name;
        Unit = unit;
        Amount = initialAmount;
    }

    public void IncreaseAmount(double toIncrease)
    {
        Amount += toIncrease;
    }

    public void DecreaseAmount(double toDecrease)
    {
        if (toDecrease > Amount)
        {
            throw new IngredientExhausted(this);
        }
        Amount -= toDecrease;
    }
}

public enum Unit
{
    Grams,
    Pieces,
    Liters,
    Packs
}

public class IngredientExhausted : Exception
{
    public IngredientExhausted(Ingredient ingredient): base($"{ingredient.Name} is exhausted")
    {
    }
}
