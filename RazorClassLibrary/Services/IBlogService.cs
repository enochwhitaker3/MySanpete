using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Services;

public interface IBlogService
{
    public Task<BlogDTO?> AddBlog(AddBlogRequest request);
    public Task<BlogDTO?> EditBlog(UpdateBlogRequest blogRequest);
    public Task<bool> DeleteBlog(int id);
    public Task<BlogDTO?> GetBlog(int id);
    public Task<List<BlogDTO>> GetAllBlogs();
}
