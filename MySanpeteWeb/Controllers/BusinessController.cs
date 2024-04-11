using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;

namespace MySanpeteWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BusinessController : Controller
{
    private readonly IBusinessService businessService;

    public BusinessController(IBusinessService businessService)
    {
        this.businessService = businessService;
    }

    [HttpGet()]
    public async Task<List<BusinessDTO>> GetAll()
    {
        var allBusinesses = await businessService.GetAllBusinesses();
        return allBusinesses;
    }

    [HttpGet("/byid/{id}")]
    public async Task<BusinessDTO?> GetById(int id)
    {
        var business = await businessService.GetBusiness(id);
        if (business is null)
        {
            return null;
        }
        return business;
    }

    [HttpGet("/byemail/{email}")]
    public async Task<BusinessDTO?> GetByEmail(string email)
    {
        var business = await businessService.GetBusiness(email);
        if (business is null)
        {
            return null;
        }
        return business;
    }
}
