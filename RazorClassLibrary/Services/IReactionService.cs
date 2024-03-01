using RazorClassLibrary.Data;
using RazorClassLibrary.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Services;

public interface IReactionService
{
    public Task<BlogReaction> AddBlogReaction(AddReactionRequest request);
    public Task<bool> DeleteBlogReaction(int id);
    public Task<PodcastReaction> AddPodReaction(AddReactionRequest request);
    public Task<bool> DeletePodReaction(int id);
}
