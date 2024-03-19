using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public Guid? Guid { get; set; }
    public string? Username { get; set; }
    public string? UserEmail { get; set; }
    public byte[]? Photo { get; set; }
}
