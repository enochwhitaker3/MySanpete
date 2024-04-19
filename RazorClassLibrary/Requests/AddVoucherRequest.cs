using RazorClassLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Requests;

public class AddVoucherRequest
{
    public int BusinessId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? PromoCode { get; set; }
    public string? PromoDescription { get; set; }
    public string? PromoName { get; set; }
    public int? PromoStock { get; set; }
    public Decimal? RetailPrice { get; set; }
    public int? TotalReclaimable { get; set; }

    public string? StripeId { get; set; }
    public string? PriceId { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsBundle { get; set; }
}
