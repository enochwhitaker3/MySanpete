﻿using Microsoft.EntityFrameworkCore;
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

        var context = await dbContextFactory.CreateDbContextAsync();

        var authorCapable = await context.EndUsers
            .Where(x => x.Id == request.AuthorId && x.UserRoleId <= 2 && x.UserRoleId > 0)
            .FirstOrDefaultAsync();

        if (authorCapable == null)
        {
            throw new Exception("The author either doesn't exist or doesn't have permissions");
        }

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
            context.Blogs.Remove(bud);
            await context.SaveChangesAsync();

            return true;
        }
        return false;
    }

    public async Task<BlogDTO?> EditBlog(BlogDTO blogDto)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var buc = await context.Blogs
            .Include(x => x.BlogComments)
                .ThenInclude(x => x.Comment)
            .Include(x => x.BlogReactions)
                .ThenInclude(x => x.Reaction)
            .Include(x => x.Author)
            .Where(x => x.Id == blogDto.Id)
            .FirstOrDefaultAsync();
        if (buc != null)
        {
            buc.Title = blogDto.Title ?? "Default Title";
            buc.PublishDate = blogDto.PublishDate;
            buc.Commentable = blogDto.Commentable;
            buc.BlogContent = blogDto.Content ?? "Default Content";
            buc.Photo = blogDto.Photo;

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
            .Include(x => x.BlogReactions)
                .ThenInclude(x => x.Reaction)
            .Include(x => x.Author)
            .Select(x => x.ToDto()).ToListAsync();
    }

    public async Task<BlogDTO?> GetBlog(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var blog = await context.Blogs
            .Include(x => x.BlogComments)
                .ThenInclude(x => x.Comment)
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
