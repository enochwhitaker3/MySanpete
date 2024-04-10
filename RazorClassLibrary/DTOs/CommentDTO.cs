

using RazorClassLibrary.Data;

namespace RazorClassLibrary.DTOs;

public class CommentDTO
{
    public int Id { get; set; }
    public int? ReplyId { get; set; }
    public string? Content { get; set; }

    public string? UserName { get; set; }

    public string? UserPhotoURL { get; set; }

    public DateTime? PostedDate { get; set; }

    public IEnumerable<CommentDTO>? Replies { get; set; }
}
