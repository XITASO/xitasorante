namespace Core;

public enum Country
{
    Germany,
    Italy,
    France,
}

public class CalculationService : ICalculationService
{
    private Country? country;
    private readonly IList<InvoicePosition> invoicePositions = new List<InvoicePosition>();

    private IList<decimal> tips;

    public void AddInvoicePosition(Dish dish)
    {
        invoicePositions.Add(new InvoicePosition
        {
            Name = dish.Title,
            TaxRate = GetTaxByCountry(),
            Total = dish.PriceInEuro,
        });
    }

    public void ReceivedTip(double tip)
    {
        try
        {
            if (tips.Count > 0)
            {
                tips.Add((decimal) tip);
            }
        }
        catch (Exception e)
        {
            // tips is not initialized -> first element
            tips = new List<decimal>();
            tips.Add((decimal) tip);
        }
    }

    public void SetTaxFor(Country c)
    {
        country = c;
    }

    public Invoice SumUp()
    {
        if (country is null)
        {
            throw new Exception("Country not set");
        }

        var taxByCountry = GetTaxByCountry();
        var positionsTotal = invoicePositions.Sum(ip => ip.NetTotal);
        return new Invoice
        {
            InvoicePositions = invoicePositions,
            Total = positionsTotal * (1 + taxByCountry)
        };
    }

    private decimal GetTaxByCountry()
    {
        if (country is null)
        {
            throw new Exception("Country not set");
        }

        switch (country)
        {
            case Country.Germany:
                return new decimal(0.19);
            case Country.Italy:
                return (decimal) 0.04;
            case Country.France:
                return 0.196m;
            default:
                throw new Exception("Country does not exist!");
        }
    }
}