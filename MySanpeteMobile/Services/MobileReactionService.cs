using RazorClassLibrary.Data;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System.Net.Http.Json;

namespace MySanpeteMobile.Services;

public class MobileReactionService : IReactionService
{
    private readonly HttpClient httpClient;
    public MobileReactionService(HttpClient HttpClient)
    {
        httpClient = HttpClient;
    }
    public async Task<BlogReaction?> AddBlogReaction(AddReactionRequest request)
    {
        var result = await httpClient.PostAsJsonAsync<AddReactionRequest>($"/api/reaction/addblogreaction/{request}", request);
        return await result.Content.ReadFromJsonAsync<BlogReaction?>();
    }

    public async Task<PodcastReaction?> AddPodReaction(AddReactionRequest request)
    {
        var result = await httpClient.PostAsJsonAsync<AddReactionRequest>($"/api/reaction/addpodcastreaction/{request}", request);
        return await result.Content.ReadFromJsonAsync<PodcastReaction?>();
    }

    public async Task<bool> DeleteBlogReaction(int id)
    {
        var result = await httpClient.DeleteFromJsonAsync<bool>($"/api/reaction/deleteblogreaction/{id}");
        return result;
    }

    public async Task<bool> DeletePodReaction(int id)
    {
        var result = await httpClient.DeleteFromJsonAsync<bool>($"api/reaction/deletepodcastreaction/{id}");
        return result;
    }
}
