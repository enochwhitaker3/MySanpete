using System;
using System.Collections.Generic;

namespace RazorClassLibrary.Data;

public partial class PodcastReaction
{
    public int Id { get; set; }

    public int PodcastId { get; set; }

    public int ReactionId { get; set; }

    public int? UserId { get; set; }

    public virtual Podcast Podcast { get; set; } = null!;

    public virtual Reaction Reaction { get; set; } = null!;

    public virtual EndUser? User { get; set; }
}
