
namespace RazorClassLibrary.Requests;

public class UpdateBusinessRequest
{
    public int Id { get; set; }
    public string? BusinessName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? WebsiteURL { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }
    public byte[]? Logo { get; set; }
}
