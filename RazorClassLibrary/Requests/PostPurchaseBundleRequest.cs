using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Requests;

public class PostPurchaseBundleRequest
{
    public int BundleId { get; set; }
    public Guid UserId { get; set; }
    public string? ChargeId { get; set; }
}
