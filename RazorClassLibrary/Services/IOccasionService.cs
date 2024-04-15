using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Services;

public interface IOccasionService
{
    public Task<OccasionDTO> GetOccasion(int id);
    public Task<List<OccasionDTO>> GetAllOcassions();
    public Task<OccasionDTO> AddOccasion(AddOccasionRequest request);
    public Task<bool> DeleteOccasion(int id);
    public Task<OccasionDTO> UpdateOccasion(UpdateOccasionRequest occasion);

}
