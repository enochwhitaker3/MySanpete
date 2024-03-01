using Microsoft.EntityFrameworkCore;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;

namespace MySanpeteWeb.Services;

public class WebPodcastService : IPodcastService
{
    private readonly IDbContextFactory<MySanpeteDbContext> dbContextFactory;
    public WebPodcastService(IDbContextFactory<MySanpeteDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }
    public async Task AddPodcast(AddPodcastRequest request)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        Podcast podcast = new Podcast()
        {
            Commentable = request.Commentable,
            PodcastUrl = request.URL,
            PodcastName = request.PodcastName,
        };
        await context.Podcasts.AddAsync(podcast);
        await context.SaveChangesAsync();
    }

    public async Task DeletePodcast(int podcastId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var dub = await context.Podcasts.Where(x => x.Id == podcastId).FirstOrDefaultAsync();
        if (dub != null)
        {
            context.Podcasts.Remove(dub);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<PodcastDTO>> GetAllPodcasts()
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Podcasts
              .Include(x => x.PodcastComments)
                .ThenInclude(x => x.Comment)  // DUSTY COMMENT not sure about this line 
              .Include(x => x.PodcastReactions)
              .Select(x => x.ToDto()).ToListAsync();
    }

    public async Task<PodcastDTO> GetPodcast(int podcastId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var podcast = await context.Podcasts
              .Include(x => x.PodcastComments)
                .ThenInclude(x => x.Comment)  // DUSTY COMMENT not sure about this line 
              .Include(x => x.PodcastReactions)
                .ThenInclude(x => x.Reaction)
              .Select(x => x.ToDto())
              .Where(x => x.Id == podcastId)
              .FirstOrDefaultAsync();

        if (podcast != null)
        {
            return podcast;
        }

        return new PodcastDTO();
    }
}

