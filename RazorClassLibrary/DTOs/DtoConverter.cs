using RazorClassLibrary.Data;

namespace RazorClassLibrary.DTOs;

public static class DtoConverter
{
    public static UserDTO ToDto(this EndUser user)
    {
        return new UserDTO()
        {
            Id = user.Id,
            Guid = user.Guid,
            Username = user.UserName,
            UserEmail = user.UserEmail,
            Photo = user.Photo,
        };
    }
}
