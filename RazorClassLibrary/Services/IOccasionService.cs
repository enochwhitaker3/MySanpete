using RazorClassLibrary.Data;
using RazorClassLibrary.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Services;

public interface IOccasionService
{
    public Task<Occasion> GetOccasion(int id);
    public Task<List<Occasion>> GetAllOcassions();
    public Task<Occasion> AddOccasion(AddOccasionRequest request);
    public Task<bool> DeleteOccasion(int id);
    public Task<Occasion> UpdateOccasion(Occasion occasion);

}
