﻿using Microsoft.EntityFrameworkCore;
using RazorClassLibrary.Data;
using RazorClassLibrary.Pages;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;

namespace MySanpeteWeb.Services;

public class WebOccasionService : IOccasionService
{
    private IDbContextFactory<MySanpeteDbContext> dbContextFactory;
    public WebOccasionService(IDbContextFactory<MySanpeteDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<Occasion> AddOccasion(AddOccasionRequest request)
    {
        if (request.BusinessId == 0)
        {
            throw new Exception("Occasions need a business");
        }

        if (request.Title is null)
        {
            throw new Exception("Occasions need a title");
        }

        if (request.Description is null)
        {
            throw new Exception("Occasions need a description");
        }

        if (request.EndDate < request.StartDate)
        {
            throw new Exception("The end date of the even needs to be after the start date");
        }

        if (request.XCoordinate is null)
        {
            throw new Exception("The occasion needs an X coordinate");
        }

        if (request.YCoordinate is null)
        {
            throw new Exception("The occasion needs an Y coordinate");
        }

        using var context = await dbContextFactory.CreateDbContextAsync();

        var newOccasion = new Occasion()
        {
            Title = request.Title,
            BusinessId = request.BusinessId,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            XCoordinate = request.XCoordinate,
            YCoordinate = request.YCoordinate,
            Photo = request.Photo,
        };

        await context.Occasions.AddAsync(newOccasion);
        await context.SaveChangesAsync();
        return newOccasion;
    }

    public async Task<bool> DeleteOccasion(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var isExist = await context.Occasions.Where(o => o.Id == id).FirstOrDefaultAsync();
        if (isExist is null)
        {
            throw new Exception("No occasion found with given ID");
        }

        context.Occasions.Remove(isExist);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Occasion>> GetAllOcassions()
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        return await context.Occasions.ToListAsync();
    }

    public async Task<Occasion> GetOccasion(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var isExist = await context.Occasions.Where(o => o.Id == id).FirstOrDefaultAsync();

        if (isExist is null)
        {
            throw new Exception("No occasion was found with given ID");
        }

        return isExist;
    }

    public async Task<Occasion> UpdateOccasion(Occasion occasion)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var occasionToPatch = await context.Occasions.Where(o => o.Id == occasion.Id).FirstOrDefaultAsync();

        if (occasionToPatch is null)
        {
            throw new Exception("No occasion was found with given ID");
        }

        occasionToPatch.Business = occasion.Business;
        occasionToPatch.Title = occasion.Title;
        occasionToPatch.BusinessId = occasion.BusinessId;
        occasionToPatch.Description = occasion.Description;
        occasionToPatch.StartDate = occasion.StartDate;
        occasionToPatch.EndDate = occasion.EndDate;
        occasionToPatch.XCoordinate = occasion.XCoordinate;
        occasionToPatch.YCoordinate = occasion.YCoordinate;
        occasionToPatch.Photo = occasion.Photo;

        context.Update(occasionToPatch);
        await context.SaveChangesAsync();

        return occasionToPatch;
    }
}
