using System;
using System.Collections.Generic;

namespace RazorClassLibrary.Data;

public partial class Podcast
{
    public int Id { get; set; }

    public string PodcastName { get; set; } = null!;

    public string PodcastUrl { get; set; } = null!;

    public bool Commentable { get; set; }
    public DateTime? PublishDate { get; set; }

    public virtual ICollection<PodcastComment> PodcastComments { get; set; } = new List<PodcastComment>();

    public virtual ICollection<PodcastReaction> PodcastReactions { get; set; } = new List<PodcastReaction>();
}