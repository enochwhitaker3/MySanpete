using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;

namespace MySanpeteWeb.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserVoucherController : Controller
{
    private readonly IUserVoucherService userVoucherService;

    public UserVoucherController(IUserVoucherService userVoucherService)
    {
        this.userVoucherService = userVoucherService;
    }

    [HttpGet()]
    public async Task<List<UserVoucherDTO>> GetAll()
    {
        var result = await userVoucherService.GetAllUserVouchers();
        return result;
    }

    [HttpGet("uservoucherbusiness/{businessEmail}")]
    public async Task<List<UserVoucherDTO>> GetAllByBusiness(string businessEmail)
    {
        var result = await userVoucherService.GetAllByBusiness(businessEmail);
        return result;
    }

    [HttpGet("uservouchervoucher/{voucherId}")]
    public async Task<List<UserVoucherDTO>> GetAllByVoucher(int voucherId)
    {
        var result = await userVoucherService.GetAllByVoucher(voucherId);
        return result;
    }

    [HttpGet("uservoucheruser/{userId}")]
    public async Task<List<UserVoucherDTO>> GetAllByUser(int userId)
    {
        var result = await userVoucherService.GetAllByUser(userId);
        return result;
    }

    [HttpGet("uservoucher/{voucherId}")]
    public async Task<UserVoucherDTO> GetById(int voucherId)
    {
        var result = await userVoucherService.GetById(voucherId);
        return result;
    }
}
