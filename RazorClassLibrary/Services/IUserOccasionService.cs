﻿using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;

namespace RazorClassLibrary.Services;

public interface IUserOccasionService
{
    public Task<List<UserOccasionDTO?>> GetAllUserOccasions();
    public Task<List<UserOccasionDTO?>> GetAllUserOccasionsByOccasion(int occasionId);
    public Task<List<UserOccasionDTO?>> GetAllUserOccasionsByUser(Guid? userGuid);
    public Task<UserOccasionDTO?> GetUserOccasion(int userId);
    public Task<UserOccasionDTO?> AddNewUserOccasion(AddUserOccasionRequest userOccasionRequest);
    public Task<UserOccasionDTO?> UpdateUserOccasion(UserOccasionDTO userOccasionDTO);
    public Task<bool> DeleteUserOccasion(int id);
}
