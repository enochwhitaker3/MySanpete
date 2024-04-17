using RazorClassLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.DTOs;

public class UserOccasionDTO
{
    public int Id { get; set; }
    public OccasionDTO? Occasion { get; set; }
    public UserDTO? User { get; set; }
}
