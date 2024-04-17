namespace RazorClassLibrary.Services;

public interface IGoogleApiService
{
    public Task<Coords?> GetCoordsOfAddress(string address);
}

public class Coords
{
    public float X { get; set; }
    public float Y { get; set; }
}
