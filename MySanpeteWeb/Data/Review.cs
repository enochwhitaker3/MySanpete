using System;
using System.Collections.Generic;

namespace MySanpeteWeb.Data;

public partial class Review
{
    public int Id { get; set; }

    public int BusinessId { get; set; }

    public int UserId { get; set; }

    public string ReviewText { get; set; } = null!;

    public int Stars { get; set; }

    public virtual Business Business { get; set; } = null!;

    public virtual EndUser User { get; set; } = null!;
}
