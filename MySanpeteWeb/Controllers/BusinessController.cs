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
    public async Task<List<Business>> GetAll()
    {
        var allBusinesses = await businessService.GetAllBusinesses();
        return allBusinesses;
    }

    [HttpGet("{id}")]
    public async Task<Business?> GetById(int id)
    {
        var business = await businessService.GetBusiness(id);
        if (business is null)
        {
            return null;
        }
        return business;
    }
}
