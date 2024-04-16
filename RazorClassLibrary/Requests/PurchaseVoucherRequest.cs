using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Requests;

public class PurchaseVoucherRequest
{
    public int VoucherId { get; set; }
    public string? UserId { get; set; }
    public string? PriceId { get; set; }
}
