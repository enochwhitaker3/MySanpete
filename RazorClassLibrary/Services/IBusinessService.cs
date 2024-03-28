using RazorClassLibrary.Data;
using RazorClassLibrary.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Services;

public interface IBusinessService
{
    public Task<List<Business>> GetAllBusinesses();
    public Task<Business?> GetBusiness(int id);
    public Task<Business?> AddBusiness(AddBusinessRequest request);
    public Task<Business?> UpdateBusiness(Business business);
    public Task<bool?> DeleteBusiness(int id);

}
