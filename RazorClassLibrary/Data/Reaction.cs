using System;
using System.Collections.Generic;

namespace RazorClassLibrary.Data;

public partial class Reaction
{
    public int Id { get; set; }

    public string Unicode { get; set; } = null!;

    public virtual ICollection<BlogReaction> BlogReactions { get; set; } = new List<BlogReaction>();

    public virtual ICollection<PodcastReaction> PodcastReactions { get; set; } = new List<PodcastReaction>();
}
