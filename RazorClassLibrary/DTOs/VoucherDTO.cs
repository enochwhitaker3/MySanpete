
namespace RazorClassLibrary.DTOs;

public class VoucherDTO
{
    public int Id;
    public string? BusinessName;
    public DateTime? StartDate;
    public DateTime? EndDate;
    public string? PromoCode;
    public string? PromoName;
    public string? PromoDescription;
    public int? LeftInStock;
    public Decimal? RetailPrice;
    public int? AmmountReclaimable;
    public int? Stock;
    public string? PriceId;
}
