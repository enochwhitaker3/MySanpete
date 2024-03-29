﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Requests;

public class AddBlogRequest
{
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public int AuthorId { get; set; }
    public bool Commentable { get; set; }
    public byte[]? Photo { get; set; }
}
