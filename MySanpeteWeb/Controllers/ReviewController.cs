using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;

namespace MySanpeteWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : Controller
{
    private readonly IReviewService reviewService;

    public ReviewController(IReviewService reviewService)
    {
        this.reviewService = reviewService;
    }

    [HttpGet("{businessId}")]
    public async Task<List<Review>> GetAll(int businessId)
    {
        var allReviews = await reviewService.GetAllReview(businessId);
        return allReviews;
    }

    [HttpPost("{request}")]
    public async Task<Review?> AddReview(AddReviewRequest request)
    {
        var result = await reviewService.AddReview(request);
        if (result is null)
        {
            return null;
        }
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteReview(int id)
    {
        var result = await reviewService.DeleteReview(id);
        return result;
    }
}
