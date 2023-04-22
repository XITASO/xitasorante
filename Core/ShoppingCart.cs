namespace Core;

public class ShoppingCart
{
    private readonly IOrderProcessor orderProcessor;

    public ShoppingCart(IOrderProcessor orderProcessor)
    {
        this.orderProcessor = orderProcessor;
    }

    public decimal TotalPrice() => items.Sum(i => i.PriceInEuro);

    public IList<Dish> GetItems() => items;

    private IList<Dish> items = new List<Dish>();
    
    public void AddDish(Dish dish)
    {
        items.Add(dish);
    }

    public void Order()
    {
        var order = new Order();
        foreach (var item in items)
        {
            order.AddDish(item);
        }
        orderProcessor.Process(order);
    }
    
}