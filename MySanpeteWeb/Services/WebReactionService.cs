using Microsoft.EntityFrameworkCore;
using RazorClassLibrary.Data;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;

namespace MySanpeteWeb.Services;

public class WebReactionService : IReactionService
{
    private IDbContextFactory<MySanpeteDbContext> dbContextFactory;
    public WebReactionService(IDbContextFactory<MySanpeteDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }
    public async Task<BlogReaction?> AddBlogReaction(AddReactionRequest request)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var reactions = await context.Reactions.ToListAsync();

        var reaction = await context.Reactions.FirstOrDefaultAsync(r => r.Unicode == request.Unicode);
        var blog = await context.Blogs.FirstOrDefaultAsync(b => b.Id == request.ContentId);
        var user = await context.EndUsers.FirstOrDefaultAsync(u => u.Guid == request.UserGuid);

        if (reaction is null || blog is null || user is null)
        {
            throw new Exception("reaction or blog or user is null in the request");
        }

        var blogReaction = new BlogReaction()
        {
            ReactionId = reaction.Id,
            BlogId = blog.Id,
            UserId = user.Id,
        };

        await context.BlogReactions.AddAsync(blogReaction);
        await context.SaveChangesAsync();

        return blogReaction;
    }

    public async Task<PodcastReaction?> AddPodReaction(AddReactionRequest request)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var reaction = await context.Reactions.FirstOrDefaultAsync(r => r.Unicode == request.Unicode);
        var podcast = await context.Podcasts.FirstOrDefaultAsync(p => p.Id == request.ContentId);
        var user = await context.EndUsers.FirstOrDefaultAsync(u => u.Guid == request.UserGuid);

        if (reaction is null || podcast is null || user is null)
        {
            throw new Exception("reaction or podcast or user is null in the request");
        }

        var podReaction = new PodcastReaction()
        {
            ReactionId = reaction.Id,
            PodcastId = podcast.Id,
            UserId = user.Id,
        };

        await context.PodcastReactions.AddAsync(podReaction);
        await context.SaveChangesAsync();

        return podReaction;
    }

    public async Task<bool> DeleteBlogReaction(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var all = await context.BlogReactions.ToListAsync();

        var doesExist = await context.BlogReactions.Where(b => b.Id == id).FirstOrDefaultAsync();
        if (doesExist is null)
        {
            return false;
        }

        context.BlogReactions.Remove(doesExist);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePodReaction(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var doesExist = await context.PodcastReactions.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (doesExist is null)
        {
            return false;
        }

        context.PodcastReactions.Remove(doesExist);
        await context.SaveChangesAsync();

        return true;
    }

}
