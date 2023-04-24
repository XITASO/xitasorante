namespace Core;

public interface IOrderRepository
{
    void DispatchOrder(Order order);

    Order GetOrderByNumber(ShortId orderNumber);

    Invoice PayOrder(ShortId orderNumber, double? tip);
}