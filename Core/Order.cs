using System.Collections.Immutable;
using System.Net.Sockets;

namespace Core;

public class Order
{
    private readonly Dictionary<Dish, OrderPosition> positions = new();
    public IEnumerable<OrderPosition> Positions => positions.Values.ToImmutableHashSet();

    public void AddDish(Dish toAdd)
    {
        if (positions.TryGetValue(toAdd, out var position))
        {
            position.Amount += 1;
            return;
        }

        positions.Add(toAdd, new OrderPosition(toAdd, 1));
    }
}

public class OrderPosition
{
    public Dish Dish { get; }
    public uint Amount { get; set; }

    public OrderPosition(Dish dish, uint initialAmount)
    {
        Dish = dish;
        Amount = initialAmount;
    }
}