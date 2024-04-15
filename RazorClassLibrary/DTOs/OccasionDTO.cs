using RazorClassLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.DTOs;

public class OccasionDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public decimal? XCoordinate { get; set; }
    public decimal? YCoordinate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int BusinessId { get; set; }
    public string? Description { get; set; }
    public string? PhotoURL { get; set; }
    public BusinessDTO? Business { get; set; }

}
