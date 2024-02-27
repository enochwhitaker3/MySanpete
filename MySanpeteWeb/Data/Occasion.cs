using System;
using System.Collections.Generic;

namespace MySanpeteWeb.Data;

public partial class Occasion
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public decimal? XCoordinate { get; set; }

    public decimal? YCoordinate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int BusinessId { get; set; }

    public string? Description { get; set; }

    public byte[]? Photo { get; set; }

    public virtual Business Business { get; set; } = null!;

    public virtual ICollection<UserOccasion> UserOccasions { get; set; } = new List<UserOccasion>();
}
