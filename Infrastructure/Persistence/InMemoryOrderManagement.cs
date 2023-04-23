using Core;

namespace Infrastructure.Persistence;

public class InMemoryOrderManagement : IOrderRepository
{
    private readonly Dictionary<ShortId, Order> idToOrders = new();

    public void DispatchOrder(Order order)
    {
        idToOrders.Add(order.OrderNumber, order);
    }

    public Order GetOrderByNumber(ShortId orderNumber)
    {
        return idToOrders[orderNumber]; // Id == OrderNumber
    }
}