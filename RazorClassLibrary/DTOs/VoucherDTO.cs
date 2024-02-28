
namespace RazorClassLibrary.DTOs;

public class VoucherDTO
{
    public int Id;
    public required string BusinessName;
    public DateTime? StartDate;
    public DateTime? EndDate;
    public required string PromoCode;
    public required string PromoName;
    public required string PromoDescription;
    public int? LeftInStock;
    public Decimal? RetailPrice;
    public int? AmmountReclaimable;
    public int? Stock;
}
