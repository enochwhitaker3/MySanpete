using System;
using System.Collections.Generic;

namespace RazorClassLibrary.Data;

public partial class UserVoucher
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int VoucherId { get; set; }

    public decimal? FinalPrice { get; set; }

    public bool? Isused { get; set; }

    public int TotalReclaimable { get; set; }

    public string ChargeId { get; set; } = null!;

    public DateTime PurchaseDate { get; set; }

    public string? PromoCode { get; set; }

    public DateTime LastUpdated { get; set; }

    public int TimesClaimed { get; set; }

    public virtual EndUser User { get; set; } = null!;

    public virtual Voucher Voucher { get; set; } = null!;
}
