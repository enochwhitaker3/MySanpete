using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Requests;

public class SetRoleRequest
{
    public Guid? UserId { get; set; }
    public int RoleId { get; set; }
    public string? AuthString { get; set; }
}
