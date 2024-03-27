using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Requests;

public class AddBusinessRequest
{
    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
    public byte[]? Logo { get; set; }
    public string? PhoneNum { get;set; } = null;
    public string? WebURL { get; set; } = null; 
}
