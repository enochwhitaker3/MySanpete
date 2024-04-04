using RazorClassLibrary.Data;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System.Net.Http.Json;

namespace MySanpeteMobile.Services;

internal class MobileReviewService : IReviewService
{
    private readonly HttpClient httpClient;
    public MobileReviewService(HttpClient HttpClient)
    {
        this.httpClient = HttpClient;
    }
    public async Task<Review?> AddReview(AddReviewRequest request)
    {
        var result = await httpClient.PostAsJsonAsync<AddReviewRequest>($"/api/review/{request}", request);
        return await result.Content.ReadFromJsonAsync<Review>();
    }

    public async Task<bool> DeleteReview(int reviewId)
    {
        var result = await httpClient.DeleteFromJsonAsync<bool>($"/api/review/{reviewId}");
        return result;
    }

    public async Task<List<Review>> GetAllReview(int buisnessId)
    {
        var result = await httpClient.GetFromJsonAsync<List<Review>>($"api/review/");
        return result!;
    }
}
