namespace EticaretAPI.Domain.Entities;

public class InvoiceImageFile : File
{
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
}
