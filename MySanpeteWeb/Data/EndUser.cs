using System;
using System.Collections.Generic;

namespace MySanpeteWeb.Data;

public partial class EndUser
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public int UserRoleId { get; set; }

    public byte[]? Photo { get; set; }

    public string UserEmail { get; set; } = null!;

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<UserComment> UserComments { get; set; } = new List<UserComment>();

    public virtual ICollection<UserOccasion> UserOccasions { get; set; } = new List<UserOccasion>();

    public virtual UserRole UserRole { get; set; } = null!;

    public virtual ICollection<UserVourcher> UserVourchers { get; set; } = new List<UserVourcher>();
}
