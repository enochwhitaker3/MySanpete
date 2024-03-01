using Microsoft.EntityFrameworkCore;
using MySanpeteWeb.Data;
using Npgsql.NameTranslation;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;

namespace MySanpeteWeb.Services;

public class WebBlogService : IBlogService
{
    private readonly IDbContextFactory<MySanpeteDbContext> dbContextFactory;
    public WebBlogService(IDbContextFactory<MySanpeteDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }
    public async Task AddBlog(AddBlogRequest request)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        Blog newBlog = new Blog()
        {
            Title = request.Title,
            BlogContent = request.Content,
            PublishDate = DateTime.Now,
            AuthorId = request.AuthorId,
            Commentable = request.Commentable,
            Photo = request.Photo,
        };
        await context.Blogs.AddAsync(newBlog);
        await context.SaveChangesAsync();
    }

    public async Task DeleteBlog(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var bud = await context.Blogs.Where(x => x.Id == id).FirstOrDefaultAsync();   
        if (bud != null)
        {
            context.Blogs.Remove(bud);
            await context.SaveChangesAsync();
        }
    }

    public async Task EditBlog(BlogDTO blogDto)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var buc = await context.Blogs.Where(x => x.Id == blogDto.Id).FirstOrDefaultAsync();
        if (buc != null)
        {
            buc.Author.UserName = blogDto.AuthorName;
            buc.Title = blogDto.Title ?? "Default Title";
            buc.PublishDate = blogDto.PublishDate;
            buc.Commentable = blogDto.Commentable;
            buc.Photo = blogDto.Photo;

            context.Blogs.Update(buc);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<BlogDTO>> GetAllBlogs()
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Blogs
            .Include(x => x.BlogComments)
            .Include(x => x.BlogReactions)
                .ThenInclude(x => x.Reaction)
            .Select(x => x.ToDto()).ToListAsync();
    }

    public async Task<BlogDTO> GetBlog(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var blog = await context.Blogs
            .Include(x => x.BlogComments)
                .ThenInclude(x => x.Comment)
            .Include(x => x.BlogReactions)
                .ThenInclude(x => x.Reaction)
            .Where(x => x.Id == id).FirstOrDefaultAsync();

        if (blog != null)
        {
            return blog.ToDto();
        }
        return new BlogDTO();
    }
}
