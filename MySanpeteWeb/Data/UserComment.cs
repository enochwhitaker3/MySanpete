using System;
using System.Collections.Generic;

namespace MySanpeteWeb.Data;

public partial class UserComment
{
    public int Id { get; set; }

    public int? ReplyId { get; set; }

    public int UserId { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime? PostDate { get; set; }

    public virtual ICollection<BlogComment> BlogComments { get; set; } = new List<BlogComment>();

    public virtual ICollection<UserComment> InverseReply { get; set; } = new List<UserComment>();

    public virtual ICollection<PodcastComment> PodcastComments { get; set; } = new List<PodcastComment>();

    public virtual UserComment? Reply { get; set; }

    public virtual EndUser User { get; set; } = null!;
}
