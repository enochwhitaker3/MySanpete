using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Services;

public interface IUserService
{
    public Task<UserDTO?> GetUser(string email);
    public Task<UserDTO?> GetUser(Guid guid);
    public Task<UserDTO?> AddUser(string email);
    public Task<bool> SetRole(SetRoleRequest request);
    public Task<UserDTO?> PatchUser(UserDTO user);
    public Task<bool> DeleteUser(Guid guid);
    public Task<List<UserDTO>> GetAllUsers();
    public Task<UserDTO?> GetAuthUser(string authId);
}
