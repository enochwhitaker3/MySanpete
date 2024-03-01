using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.DTOs;

public class BundleDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal FinalPrice { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<VoucherDTO>? Vouchers { get; set; }
}
