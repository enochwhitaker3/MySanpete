using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;

namespace MySanpeteWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserOccasionController : Controller
{
    private readonly IUserOccasionService userOccasionService;

    public UserOccasionController(IUserOccasionService userOccasionService)
    {
        this.userOccasionService = userOccasionService;
    }

    [HttpGet()]
    public async Task<List<UserOccasionDTO?>> GetAll()
    {
        var result = await userOccasionService.GetAllUserOccasions();
        return result;
    }

    [HttpGet("GetAllByOccasion/{occasionId}")]
    public async Task<List<UserOccasionDTO?>> GetAllByOccasion(int occasionId)
    {
        var result = await userOccasionService.GetAllUserOccasionsByOccasion(occasionId);
        return result;
    }

    [HttpGet("GetAllByUser/{userGuid}")]
    public async Task<List<UserOccasionDTO?>> GetAllByUser(Guid userGuid)
    {
        var result = await userOccasionService.GetAllUserOccasionsByUser(userGuid);
        return result;
    }

    [HttpGet("GetById/{userOccasionId}")]
    public async Task<UserOccasionDTO?> GetById(int userOccasionId)
    {
        var result = await userOccasionService.GetUserOccasion(userOccasionId);
        return result;
    }

    [HttpPost("add/{request}")]
    public async Task<UserOccasionDTO?> AddNewUserOccasion(AddUserOccasionRequest request)
    {
        var result = await userOccasionService.AddNewUserOccasion(request);
        return result;
    }

    [HttpPatch("update/{userOccasion}")]
    public async Task<UserOccasionDTO?> UpdateUserOccasion(UserOccasionDTO userOccasion)
    {
        var result = await userOccasionService.UpdateUserOccasion(userOccasion);
        return result;
    }

    [HttpDelete("delete/{userOccasionId}")]
    public async Task<bool> DeleteUserOccasion(int userOccasionId)
    {
        var result = await userOccasionService.DeleteUserOccasion(userOccasionId);
        return result;
    }
}
