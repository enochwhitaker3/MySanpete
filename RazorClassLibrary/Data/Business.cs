using System;
using System.Collections.Generic;

namespace RazorClassLibrary.Data;

public partial class Business
{
    public int Id { get; set; }

    public string BusinessName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public byte[]? Logo { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Website { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Occasion> Occasions { get; set; } = new List<Occasion>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
