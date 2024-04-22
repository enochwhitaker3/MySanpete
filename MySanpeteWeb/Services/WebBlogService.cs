using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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

    public async Task<BlogDTO?> AddBlog(AddBlogRequest request)
    {
        if (request.Title == "")
        {
            throw new Exception("Blog cannot be made with no title");
        }

        if (request.Content == "")
        {
            throw new Exception("Blog cannot be made with no content");
        }

        if (request.AuthorId <= 0)
        {
            throw new Exception("The author either doesn't exist or doesn't have permissions");
        }

        var context = await dbContextFactory.CreateDbContextAsync();

        Blog? newBlog = new Blog()
        {
            Title = request.Title,
            BlogContent = request.Content,
            PublishDate = DateTime.Now.ToUniversalTime(),
            AuthorId = request.AuthorId,
            Commentable = request.Commentable,
            Photo = request.Photo,
        };
        await context.Blogs.AddAsync(newBlog);
        await context.SaveChangesAsync();

        newBlog = await context.Blogs
            .Include(x => x.BlogComments)
                .ThenInclude(x => x.Comment)
            .Include(x => x.BlogReactions)
                .ThenInclude(x => x.Reaction)
            .Include(x => x.Author)
            .FirstOrDefaultAsync(b => b.Id == newBlog!.Id);

        return newBlog!.ToDto();
    }

    public async Task<bool> DeleteBlog(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var bud = await context.Blogs.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (bud != null)
        {
            bud.Title = "[Removed]";
            context.Blogs.Update(bud);
            await context.SaveChangesAsync();

            return true;
        }
        return false;
    }

    public async Task<BlogDTO?> EditBlog(UpdateBlogRequest updateBlog)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var buc = await context.Blogs
            .Include(x => x.BlogComments)
                .ThenInclude(x => x.Comment)
            .Include(x => x.BlogReactions)
                .ThenInclude(x => x.Reaction)
            .Include(x => x.Author)
            .Where(x => x.Id == updateBlog.Id)
            .FirstOrDefaultAsync();
        if (buc != null)
        {
            buc.Title = updateBlog.Title ?? "Default Title";
            buc.PublishDate = updateBlog.PublishDate;
            buc.Commentable = updateBlog.Commentable;
            buc.BlogContent = updateBlog.BlogContent ?? "Default Content";
            buc.Photo = updateBlog.Photo;

            context.Blogs.Update(buc);
            await context.SaveChangesAsync();

            return buc.ToDto();
        }
        return null;
    }

    public async Task<List<BlogDTO>> GetAllBlogs()
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Blogs
            .Include(x => x.BlogComments)
                .ThenInclude(x => x.Comment)
                .ThenInclude(x => x.User)
            .Include(x => x.Author)
            .Select(x => x.ToDto()).ToListAsync();
    }

    public async Task<BlogDTO?> GetBlog(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var blog = await context.Blogs
            .Include(x => x.BlogComments)
                .ThenInclude(x => x.Comment)
                .ThenInclude(x => x.User)
            .Include(x => x.BlogReactions)
                .ThenInclude(x => x.Reaction)
            .Include(x => x.Author)
            .Where(x => x.Id == id).FirstOrDefaultAsync();

        if (blog != null)
        {
            return blog.ToDto();
        }
        return null;
    }
}
