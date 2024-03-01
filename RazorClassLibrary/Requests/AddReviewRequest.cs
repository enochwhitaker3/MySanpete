using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Requests;

public class AddReviewRequest
{
    public string? Text { get; set; }
    public int Stars { get; set; }
    public int BuisnessId { get; set; }
    public Guid UserGuid { get; set; }
}
