﻿using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Pages;
using RazorClassLibrary.Requests;

namespace RazorClassLibrary.Services;

public interface IPodcastService
{
    public Task<List<PodcastDTO>> GetAllPodcasts();
    public Task<PodcastDTO> GetPodcast(int podcastId);
    public Task<PodcastDTO> AddPodcast(AddPodcastRequest request);
    public Task<bool> DeletePodcast(int podcastId);
}
