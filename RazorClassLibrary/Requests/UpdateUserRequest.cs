
namespace RazorClassLibrary.Requests;

public class UpdateUserRequest
{
    public int Id { get; set; } 
    public Guid? Guid { get; set; }
    public string? Username { get; set; }
    public string? UserEmail { get; set; }  
    public byte[]? Photo { get; set;}
}
