using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Requests;

public class AddVoucherRequest
{
    public int BusinessId;
    public DateTime StartDate;
    public DateTime? EndDate;
    public string PromoCode;
    public string PromoDescription;
    public string PromoName;
    public int PromoStock;
    public Decimal RetailPrice;
    public int TotalReclaimable;
}
