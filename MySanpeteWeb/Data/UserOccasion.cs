using System;
using System.Collections.Generic;

namespace MySanpeteWeb.Data;

public partial class UserOccasion
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int OccasionId { get; set; }

    public virtual Occasion Occasion { get; set; } = null!;

    public virtual EndUser User { get; set; } = null!;
}
