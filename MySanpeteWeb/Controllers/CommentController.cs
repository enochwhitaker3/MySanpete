using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;

namespace MySanpeteWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : Controller
{
    private readonly ICommentService commentService;
    public CommentController(ICommentService commentService)
    {
        this.commentService = commentService;
    }

    [HttpPost("addblogcomment/{request}")]
    public async Task<bool> AddBlogComment(AddCommentRequest request)
    {
        var result = await commentService.AddBlogComment(request);
        return result;
    }

    [HttpPost("addpodcastcomment/{request}")]
    public async Task<bool> AddPodcastComment(AddCommentRequest request)
    {
        var result = await commentService.AddPodcastComment(request);
        return result;
    }

    [HttpDelete("deleteblogcomment/{id}")]
    public async Task<bool> DeleteBlogComment(int id)
    {
        var result = await commentService.DeleteBlogComment(id);
        return result;
    }

    [HttpDelete("deletepodcastcomment/{id}")]
    public async Task<bool> DeletePodcastComment(int id)
    {
        var result = await commentService.DeletePodcastComment(id);
        return result;
    }
}
