using System;
using System.Collections.Generic;

namespace RazorClassLibrary.Data;

public partial class UserVourcher
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int VoucherId { get; set; }

    public decimal? FinalPrice { get; set; }

    public virtual EndUser User { get; set; } = null!;

    public virtual Voucher Voucher { get; set; } = null!;
}
