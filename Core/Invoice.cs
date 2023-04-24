namespace Core;

public class Invoice
{
    public ShortId InvoiceNumber { get; } = new();
    public DateTime Date => DateTime.Now;
    public IList<InvoicePosition> InvoicePositions { get; set; } = new List<InvoicePosition>();
    public decimal Total { get; set; }
}

public class InvoicePosition
{
    public string Name { get; set; }
    public decimal TaxRate { get; set; }

    public decimal NetTotal => Total - Tax;
    public decimal Tax => Total * TaxRate;
    public decimal Total { get; set; }
}