using RazorClassLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Requests;

public class AddUserOccasionRequest
{
    public int OccasionId { get; set; }
    public int UserId { get; set; }
}
