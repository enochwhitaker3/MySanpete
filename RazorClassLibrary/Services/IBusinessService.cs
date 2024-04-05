using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;

namespace RazorClassLibrary.Services;

public interface IBusinessService
{
    public Task<List<BusinessDTO>> GetAllBusinesses();
    public Task<BusinessDTO?> GetBusiness(int id);
    public Task<BusinessDTO?> GetBusiness(string email);
    public Task<BusinessDTO?> AddBusiness(AddBusinessRequest request);
    public Task<BusinessDTO?> UpdateBusiness(UpdateBusinessRequest businessRequest);
    public Task<bool?> DeleteBusiness(int id);

}
