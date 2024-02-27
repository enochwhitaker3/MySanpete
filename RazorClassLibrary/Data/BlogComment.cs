using System;
using System.Collections.Generic;

namespace RazorClassLibrary.Data;

public partial class BlogComment
{
    public int Id { get; set; }

    public int CommentId { get; set; }

    public int BlogId { get; set; }

    public virtual Blog Blog { get; set; } = null!;

    public virtual UserComment Comment { get; set; } = null!;
}
