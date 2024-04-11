
namespace RazorClassLibrary.Requests;

public class UpdateBusinessRequest
{
    public int Id { get; set; }
    public string? BusinessName { get; set; }
    public string? Address { get; set; }
    public byte[]? Logo { get; set; }
}
