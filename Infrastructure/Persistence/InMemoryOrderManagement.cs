using Core;

namespace Infrastructure.Persistence;

public class InMemoryOrderManagement : IOrderRepository
{
    private readonly ICalculationService calculationService;

    public InMemoryOrderManagement(ICalculationService calculationService)
    {
        this.calculationService = calculationService;
    }
    
    private readonly Dictionary<ShortId, Order> idToOrders = new();

    public void DispatchOrder(Order order)
    {
        idToOrders.Add(order.OrderNumber, order);
    }

    public Invoice PayOrder(ShortId orderNumber, double? tip)
    {
        var order = idToOrders[orderNumber];
        idToOrders[orderNumber].Payed = true;
        
        // Tax needs to be set first!
        calculationService.SetTaxFor(Country.Germany); // Currently we are only in germany 
        
        foreach (var orderPosition in order.Positions)
        {
            calculationService.AddInvoicePosition(orderPosition.Dish);
        }

        if (tip is not null)
        {
            calculationService.ReceivedTip(tip.Value);
        }

        return calculationService.SumUp();
    }

    public Order GetOrderByNumber(ShortId orderNumber)
    {
        return idToOrders[orderNumber]; // Id == OrderNumber
    }
}