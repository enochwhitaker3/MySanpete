namespace RazorClassLibrary.DTOs;

public class VoucherDTO
{
    public int Id { get; set; }
    public string? BusinessName { get; set; }
    public string? BusinessLogoURL { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? PromoCode { get; set; }
    public string? PromoName { get; set; }
    public string? PromoDescription { get; set; }
    public int? LeftInStock { get; set; }
    public Decimal? RetailPrice { get; set; }
    public int? AmmountReclaimable { get; set; }
    public int? Stock { get; set; }
    public string? PriceId { get; set; }
    public string? StripeId { get; set; }
}
