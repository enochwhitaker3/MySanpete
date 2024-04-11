using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System.Net.Http.Json;

namespace MySanpeteMobile.Services;

public class MobileBlogService : IBlogService
{
    private readonly HttpClient httpClient;
    public MobileBlogService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public Task<BlogDTO?> AddBlog(AddBlogRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBlog(int id)
    {
        throw new NotImplementedException();
    }

    public Task<BlogDTO?> EditBlog(UpdateBlogRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<List<BlogDTO>> GetAllBlogs()
    {
        var result = await httpClient.GetFromJsonAsync<List<BlogDTO>>("/api/blog");
        return result!;
    }

    public async Task<BlogDTO?> GetBlog(int id)
    {
        var result = await httpClient.GetFromJsonAsync<BlogDTO>($"/api/blog/{id}");
        return result!;
    }
}
