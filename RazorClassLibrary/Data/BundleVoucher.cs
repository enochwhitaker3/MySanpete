using System;
using System.Collections.Generic;

namespace RazorClassLibrary.Data;

public partial class BundleVoucher
{
    public int Id { get; set; }

    public int? VoucherId { get; set; }

    public int? BundleId { get; set; }

    public virtual Bundle? Bundle { get; set; }

    public virtual Voucher? Voucher { get; set; }
}
