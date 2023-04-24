namespace Core;

public interface ICalculationService
{
    void AddInvoicePosition(Dish d);
    void ReceivedTip(double tip);
    void SetTaxFor(Country country);
    Invoice SumUp();
}