using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Requests;

public class AddCommentRequest
{
    public int? replyId { get; set; }
    public Guid userGuid { get; set; }
    public int contentId { get; set; }
    public string content { get; set; } = "";
}
