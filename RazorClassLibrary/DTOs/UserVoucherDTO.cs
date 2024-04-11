namespace RazorClassLibrary.DTOs;

public class UserVoucherDTO
{
    public int Id { get; set; }
    public UserDTO? User { get; set; }
    public VoucherDTO? Voucher { get; set; }
    public Decimal? Final_Price { get; set; }
    public bool? Is_Used { get; set; }
    public int? Times_Claimed { get; set; }
    public int? Total_Reclaimable { get; set; }
    public string? Charge_Id { get; set; }
    public DateTime? Purchase_Date { get; set; }
    public string? Promo_Code { get; set; }
    public DateTime? Last_Updated { get; set; }
}
