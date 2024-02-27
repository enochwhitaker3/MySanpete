using System;
using System.Collections.Generic;

namespace MySanpeteWeb.Data;

public partial class BlogReaction
{
    public int Id { get; set; }

    public int BlogId { get; set; }

    public int ReactionId { get; set; }

    public virtual Blog Blog { get; set; } = null!;

    public virtual Reaction Reaction { get; set; } = null!;
}
