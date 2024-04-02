using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Requests;

public class AddUserVoucherRequest
{
    public int userId { get; set; }
    public int voucherId { get; set;}
    public string? chargeId { get; set; }
    public decimal finalPrice { get; set; } 
}
