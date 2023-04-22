namespace Core;

public enum Country
{
    Germany,
    Italy,
    France,
}

interface ICalculationService
{
    void AddOrderPrice(decimal price);
    void AddCostsOfGoods(decimal price);
    void ReceivedTip(double tip);
    void SetTaxFor(Country country);
    decimal SumUp();
}

public class CalculationService : ICalculationService
{
    private Country? _country;
    private readonly IList<decimal> _pizzaPrices = new List<decimal>();

    /// <summary>
    /// Vorallem Einkäufe am Wochenmarkt
    /// </summary>
    private IList<decimal> CostOfGoods = new List<decimal>();

    private IList<decimal> tips;

    public void AddOrderPrice(decimal price)
    {
        _pizzaPrices.Add(price);
    }

    public void AddCostsOfGoods(decimal price)
    {
        CostOfGoods.Add(price);
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

    public void SetTaxFor(Country country)
    {
        _country = country;
    }

    public decimal SumUp()
    {
        if (_country is null)
        {
            throw new Exception("Country not set");
        }

        var taxByCountry = GetTaxByCountry();

        var combined = _pizzaPrices.Concat(CostOfGoods).Concat(tips);
        var sumWithoutTax = combined.Sum() * (1 + taxByCountry);
        return sumWithoutTax;
    }

    private decimal GetTaxByCountry()
    {
        if (_country is null)
        {
            throw new Exception("Country not set");
        }

        switch (_country)
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