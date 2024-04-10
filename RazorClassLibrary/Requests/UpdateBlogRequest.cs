using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Requests;

public class UpdateBlogRequest
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string BlogContent { get; set; } = null!;

    public DateTime? PublishDate { get; set; }

    public int AuthorId { get; set; }

    public bool Commentable { get; set; }

    public byte[]? Photo { get; set; }
}
