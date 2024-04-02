﻿

using RazorClassLibrary.Data;

namespace RazorClassLibrary.DTOs;

public class CommentDTO
{
    public int Id { get; set; }
    public int? ReplyId { get; set; }
    public string? Content { get; set; }

    public int? UserId { get; set; }

    public IEnumerable<CommentDTO>? Replies { get; set; }
}
