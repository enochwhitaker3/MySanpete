using RazorClassLibrary.Data;
using System;
using System.Collections.Generic;

namespace RazorClassLibrary.Data;

public partial class Voucher
{
    public int Id { get; set; }

    public int BusinessId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string PromoCode { get; set; } = null!;

    public string PromoName { get; set; } = null!;

    public string PromoDescription { get; set; } = null!;

    public int? PromoStock { get; set; }

    public decimal? RetailPrice { get; set; }

    public int? TotalReclaimable { get; set; }

    public string? StripeId { get; set; }

    public string? PriceId { get; set; }

    public virtual ICollection<BundleVoucher> BundleVouchers { get; set; } = new List<BundleVoucher>();

    public virtual Business Business { get; set; } = null!;

    public virtual ICollection<UserVoucher> UserVouchers { get; set; } = new List<UserVoucher>();
}
