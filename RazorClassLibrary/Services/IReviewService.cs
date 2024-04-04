using RazorClassLibrary.Data;
using RazorClassLibrary.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Services;

public interface IReviewService
{
    public Task<List<Review>> GetAllReview(int buisnessId);
    public Task<Review?> AddReview(AddReviewRequest request);
    public Task<bool> DeleteReview(int reviewId);
}
