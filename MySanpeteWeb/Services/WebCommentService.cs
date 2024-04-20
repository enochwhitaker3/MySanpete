using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RazorClassLibrary.Data;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;

namespace MySanpeteWeb.Services;

public class WebCommentService : ICommentService
{
    private readonly IDbContextFactory<MySanpeteDbContext> dbContextFactory;
    public WebCommentService(IDbContextFactory<MySanpeteDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<bool> AddBlogComment(AddCommentRequest request)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        if (request.content == "" || request.content is null)
        {
            throw new Exception("A comment needs content");
        }

        var blogExists = await context.Blogs.AnyAsync(b => b.Id == request.contentId);
        if (!blogExists)
        {
            return false;
        }

        if (request.replyId is not null)
        {
            var commentExists = await context.UserComments.AnyAsync(bc => bc.Id == request.replyId);
            if (!commentExists)
            {
                return false;
            }
        }

        var user = await context.EndUsers.Where(u => u.Guid == request.userGuid).FirstOrDefaultAsync();
        if (user is null)
        {
            return false;
        }

        var newComment = new UserComment()
        {
            UserId = user.Id,
            CommentText = request.content,
            PostDate = DateTime.Now.ToUniversalTime(),
            ReplyId = request.replyId,
        };

        await context.UserComments.AddAsync(newComment);
        await context.SaveChangesAsync();

        var newBlogComment = new BlogComment()
        {
            BlogId = request.contentId,
            CommentId = newComment.Id
        };

        await context.BlogComments.AddAsync(newBlogComment);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddPodcastComment(AddCommentRequest request)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        if (request.content == "" || request.content is null)
        {
            throw new Exception("A comment needs content");
        }

        var podExists = await context.Podcasts.AnyAsync(b => b.Id == request.contentId);
        if (!podExists)
        {
            return false;
        }

        if (request.replyId is not null)
        {
            var commentExists = await context.UserComments.AnyAsync(bc => bc.Id == request.replyId);
            if (!commentExists)
            {
                return false;
            }
        }

        var user = await context.EndUsers.Where(u => u.Guid == request.userGuid).FirstOrDefaultAsync();
        if (user is null)
        {
            return false;
        }

        var newComment = new UserComment()
        {
            UserId = user.Id,
            CommentText = request.content,
            PostDate = DateTime.Now.ToUniversalTime(),
            ReplyId = request.replyId,
        };

        await context.UserComments.AddAsync(newComment);
        await context.SaveChangesAsync();

        var newPodComment = new PodcastComment()
        {
            PodcastId = request.contentId,
            CommentId = newComment.Id
        };

        await context.PodcastComments.AddAsync(newPodComment);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteBlogComment(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        //Blog comment under deletion
        var bcud = await context.BlogComments
            .Include(u => u.Comment)
            .FirstOrDefaultAsync(bc => bc.Id == id);

        if (bcud is null)
        {
            return false;
        }

        bcud.Comment.CommentText = "[Deleted]";
        bcud.Comment.UserId = 1;

        context.UserComments.Update(bcud.Comment);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePodcastComment(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        //pod comment under deletion
        var pcud = await context.PodcastComments
            .Include(u => u.Comment)
            .FirstOrDefaultAsync(bc => bc.Id == id);

        if (pcud is null)
        {
            return false;
        }

        pcud.Comment.CommentText = "[Redacted]";
        pcud.Comment.UserId = 1;


        context.UserComments.Update(pcud.Comment);
        await context.SaveChangesAsync();

        return true;
    }
}
