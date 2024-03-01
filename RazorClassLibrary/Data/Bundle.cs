using System;
using System.Collections.Generic;

namespace RazorClassLibrary.Data;

public partial class Bundle
{
    public int Id { get; set; }

    public string? BundleName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<BundleVoucher> BundleVouchers { get; set; } = new List<BundleVoucher>();
}
