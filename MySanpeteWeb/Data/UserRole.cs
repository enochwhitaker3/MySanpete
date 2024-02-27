using System;
using System.Collections.Generic;

namespace MySanpeteWeb.Data;

public partial class UserRole
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<EndUser> EndUsers { get; set; } = new List<EndUser>();
}
