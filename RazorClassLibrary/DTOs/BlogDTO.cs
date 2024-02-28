using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.DTOs;

public class BlogDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? AuthorName { get; set; }
    public DateTime? PublishDate { get; set; }
    public List<CommentDTO>? Comments { get; set; }

    // TODO: Dusty add Reaction
    //public List<Reaction> Reactions { get; set; }

    public bool Commentable { get; set; }
    public Byte[]? Photo { get; set; }


}
