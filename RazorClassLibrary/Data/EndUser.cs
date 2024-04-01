using System;
using System.Collections.Generic;

namespace RazorClassLibrary.Data;

public partial class EndUser
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public byte[]? Photo { get; set; }

    public string UserEmail { get; set; } = null!;

    public bool? Isactive { get; set; }

    public Guid? Guid { get; set; }

    public string? Authid { get; set; }

    public virtual ICollection<BlogReaction> BlogReactions { get; set; } = new List<BlogReaction>();

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<PodcastReaction> PodcastReactions { get; set; } = new List<PodcastReaction>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<UserComment> UserComments { get; set; } = new List<UserComment>();

    public virtual ICollection<UserOccasion> UserOccasions { get; set; } = new List<UserOccasion>();

    public virtual ICollection<UserVoucher> UserVouchers { get; set; } = new List<UserVoucher>();
}