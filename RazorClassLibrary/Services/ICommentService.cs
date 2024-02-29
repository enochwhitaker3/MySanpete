using RazorClassLibrary.Data;
using RazorClassLibrary.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Services;

public interface ICommentService
{
    public Task<bool> AddBlogComment(AddCommentRequest request);
    public Task<bool> AddPodcastComment(AddCommentRequest request);
    public Task<bool> DeleteBlogComment(int id);
    public Task<bool> DeletePodcastComment(int id);
}
