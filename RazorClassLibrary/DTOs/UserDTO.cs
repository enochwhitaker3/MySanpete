namespace RazorClassLibrary.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public Guid? Guid { get; set; }
    public string? Username { get; set; }
    public string? UserEmail { get; set; }
    public byte[]? Photo { get; set; }
}
