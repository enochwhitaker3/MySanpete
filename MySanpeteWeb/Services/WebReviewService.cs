using Microsoft.EntityFrameworkCore;
using RazorClassLibrary.Data;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;

namespace MySanpeteWeb.Services;

public class WebReviewService : IReviewService
{
    private readonly IDbContextFactory<MySanpeteDbContext> dbContextFactory;
    public WebReviewService(IDbContextFactory<MySanpeteDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }
    public async Task<Review> AddReview(AddReviewRequest request)
    {
        if (request.Text is null || request.Stars < 0 || request.Stars > 5)
        {
            throw new Exception("Sommething went wrong with your request");
        }

        using var context = await dbContextFactory.CreateDbContextAsync();

        var userMakingReview = await context.EndUsers.FirstOrDefaultAsync(u => u.Guid == request.UserGuid);

        if (userMakingReview is null)
        {
            throw new Exception("Cannot create review from an incorrect user");
        }

        var buisnessToReview = await context.Businesses.FirstOrDefaultAsync(b => b.Id == request.BuisnessId);

        if (buisnessToReview is null)
        {
            throw new Exception("Cannot create review of buisness that doesn't exist");
        }

        var newReview = new Review()
        {
            ReviewText = request.Text,
            Stars = request.Stars,
            UserId = userMakingReview.Id,
            BusinessId = buisnessToReview.Id
        };

        await context.Reviews.AddAsync(newReview);
        await context.SaveChangesAsync();

        return newReview;
    }

    public async Task<bool> DeleteReview(int reviewId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var reviewToDelete = await context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);

        if (reviewToDelete is null)
        {
            throw new Exception("Cannot delete something that doens't exist");
        }

        context.Reviews.Remove(reviewToDelete);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Review>> GetAllReview(int buisnessId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var reviews = await context.Reviews
                        .Include(r => r.Business)
                        .Include(r => r.User)
                        .Where(r => r.BusinessId == buisnessId)
                        .ToListAsync();

        return reviews;
    }
}
