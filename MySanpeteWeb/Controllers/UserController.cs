using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;
using System.Security.Cryptography.X509Certificates;

namespace MySanpeteWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet()]
    public async Task<List<UserDTO>> GetAll()
    {
        var result = await userService.GetAllUsers();
        return result;
    }

    [HttpGet("getauthuser/{authId}")]
    public async Task<UserDTO?> GetAuthUser(string authId)
    {
        authId = authId.Replace("%7C", "|");
        var result = await userService.GetAuthUser(authId);
        if (result is null)
        {
            return null;
        }
        return result;
    }

    [HttpGet("getuseremail/{email}")]
    public async Task<UserDTO?> GetUser(string email)
    {
        var result = await userService.GetUser(email);
        if (result is null)
        {
            return null;
        }
        return result;
    }

    [HttpGet("getuserguid/{guid}")]
    public async Task<UserDTO?> GetUser(Guid guid)
    {
        var result = await userService.GetUser(guid);
        if (result is null)
        {
            return null;
        }
        return result;
    }
}
