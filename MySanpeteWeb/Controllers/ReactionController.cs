using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;

namespace MySanpeteWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReactionController : Controller
{
    private readonly IReactionService reactionService;

	public ReactionController(IReactionService reactionService)
	{
		this.reactionService = reactionService;
	}

	[HttpPost("addblogreaction/{request}")]
	public async Task<BlogReaction> AddBlogReaction(AddReactionRequest request)
	{
		var result = await reactionService.AddBlogReaction(request);
		return result;
	}

	[HttpPost("addpodcastreaction/{request}")]
	public async Task<PodcastReaction> AddPodcastReaction(AddReactionRequest request)
	{
		var result = await reactionService.AddPodReaction(request); 
		return result;
	}

	[HttpDelete("deleteblogreaction/{id}")]
	public async Task<bool> DeleteBlogReaction(int id)
	{
		var result = await reactionService.DeleteBlogReaction(id);
		return result;
	}

	[HttpDelete("deletepodcastreaction/{id}")]
	public async Task<bool> DeletePodcastReaction(int id)
	{
		var result = await reactionService.DeletePodReaction(id);
		return result;
	}
}
