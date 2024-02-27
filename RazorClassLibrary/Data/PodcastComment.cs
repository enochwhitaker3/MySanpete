using System;
using System.Collections.Generic;

namespace RazorClassLibrary.Data;

public partial class PodcastComment
{
    public int Id { get; set; }

    public int CommentId { get; set; }

    public int PodcastId { get; set; }

    public virtual UserComment Comment { get; set; } = null!;

    public virtual Podcast Podcast { get; set; } = null!;
}
