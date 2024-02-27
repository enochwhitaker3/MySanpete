using System;
using System.Collections.Generic;

namespace RazorClassLibrary.Data;

public partial class Blog
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string BlogContent { get; set; } = null!;

    public DateTime? PublishDate { get; set; }

    public int AuthorId { get; set; }

    public bool Commentable { get; set; }

    public byte[]? Photo { get; set; }

    public virtual EndUser Author { get; set; } = null!;

    public virtual ICollection<BlogComment> BlogComments { get; set; } = new List<BlogComment>();

    public virtual ICollection<BlogReaction> BlogReactions { get; set; } = new List<BlogReaction>();
}
