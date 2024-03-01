﻿

using RazorClassLibrary.Data;

namespace RazorClassLibrary.DTOs;

public class PodcastDTO
{
    public int Id { get; set; }
    public string? URL { get; set; }
    public List<CommentDTO>? Comments { get; set; }
    public List<Reaction>? Reactions { get; set; }

}
