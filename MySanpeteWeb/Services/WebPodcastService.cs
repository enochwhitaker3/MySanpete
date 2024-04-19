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

    public async Task<PodcastDTO> AddPodcast(AddPodcastRequest request)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        if (request.URL is null || request.URL == "")
        {
            throw new Exception("Podcasts requires embedded code.");
        }

        Podcast podcast = new Podcast()
        {
            Commentable = request.Commentable,
            PodcastUrl = request.URL,
            PodcastName = request.PodcastName,
            PublishDate = DateTime.Now.ToUniversalTime(),
        };
        await context.Podcasts.AddAsync(podcast);
        await context.SaveChangesAsync();

        return podcast.ToDto();
    }

    public async Task<bool> DeletePodcast(int podcastId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var dub = await context.Podcasts.Where(x => x.Id == podcastId).FirstOrDefaultAsync();
        if (dub != null)
        {
            context.Podcasts.Remove(dub);
            await context.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<List<PodcastDTO>> GetAllPodcasts()
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        List<PodcastDTO> result = new List<PodcastDTO>();
        result = await context.Podcasts
              .Include(x => x.PodcastComments)
                .ThenInclude(x => x.Comment)
                .ThenInclude(x => x.User)
              .Include(x => x.PodcastReactions)
              .Select(x => x.ToDto()).ToListAsync();

        return result;
    }

    public async Task<PodcastDTO> GetPodcast(int podcastId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var podcast = await context.Podcasts
              .Include(x => x.PodcastComments)
                .ThenInclude(x => x.Comment)
                .ThenInclude(x => x.User)
              .Include(x => x.PodcastReactions)
                .ThenInclude(x => x.Reaction)
              .Where(x => x.Id == podcastId)
              .Select(x => x.ToDto())
              .FirstOrDefaultAsync();

        if (podcast != null)
        {
            return podcast;
        }

        return new PodcastDTO();
    }
}

