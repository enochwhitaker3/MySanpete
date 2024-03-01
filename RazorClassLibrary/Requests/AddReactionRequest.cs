using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Requests;

public class AddReactionRequest
{
    public int ContentId { get; set; }
    public string? Unicode { get; set; }
    public Guid? UserGuid { get; set; }

}
