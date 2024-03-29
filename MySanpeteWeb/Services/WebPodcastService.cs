﻿using Microsoft.EntityFrameworkCore;
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

        if (request.URL is null || request.URL == "" || !Uri.IsWellFormedUriString(request.URL, UriKind.Absolute))
        {
            throw new Exception("Podcasts requires a valid URL");
        }

        Podcast podcast = new Podcast()
        {
            Commentable = request.Commentable,
            PodcastUrl = request.URL,
            PodcastName = request.PodcastName,
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

