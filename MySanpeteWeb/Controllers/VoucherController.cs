using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;

namespace MySanpeteWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VoucherController : Controller
{
    private readonly IVoucherService voucherService;
    public VoucherController(IVoucherService voucherService)
    {
        this.voucherService = voucherService;
    }

    [HttpGet()]
    public async Task<List<VoucherDTO>> GetAll()
    {
        var allVouchers = await voucherService.GetAllVouchers();
        return allVouchers;
    }

    [HttpGet("businessvouchers/{businessid}")]
    public async Task<List<VoucherDTO>> GetAllBusiness(int businessId)
    {
        var allBusinessVouchers = await voucherService.GetAllBusinessVouchers(businessId);
        return allBusinessVouchers;
    }


    [HttpGet("{id}")]
    public async Task<VoucherDTO?> GetById(int id)
    {
        var result = await voucherService.GetVoucher(id);
        if (result is null)
        {
            return null;
        }
        return result;
    }
}
