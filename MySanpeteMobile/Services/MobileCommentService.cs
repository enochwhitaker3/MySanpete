using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System.Net.Http.Json;

namespace MySanpeteMobile.Services;

public class MobileCommentService : ICommentService
{
    private readonly HttpClient httpClient;
    public MobileCommentService(HttpClient HttpClient)
    {
        this.httpClient = HttpClient;
    }
    public async Task<bool> AddBlogComment(AddCommentRequest request)
    {
        var result = await httpClient.PostAsJsonAsync<AddCommentRequest>($"/api/comment/addblogcomment/{request}", request);
        return await result.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<bool> AddPodcastComment(AddCommentRequest request)
    {
        var result = await httpClient.PostAsJsonAsync<AddCommentRequest>($"/api/comment/addpodcastcomment/{request}", request);
        return await result.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<bool> DeleteBlogComment(int id)
    {
        var result = await httpClient.DeleteFromJsonAsync<bool>($"/api/comment/deleteblogcomment/{id}");
        return result;
    }

    public async Task<bool> DeletePodcastComment(int id)
    {
        var result = await httpClient.DeleteFromJsonAsync<bool>($"/api/comment/deletepodcastcomment/{id}");
        return result;
    }
}
