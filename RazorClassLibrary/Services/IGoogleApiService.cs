namespace RazorClassLibrary.Services;

public interface IGoogleApiService
{
    public Task<Coords?> GetCoordsOfAddress(string address);
    public Task<string?> GetAddressOfLatLon(decimal? lat, decimal? lon);
}

public class Coords
{
    public float X { get; set; }
    public float Y { get; set; }
}
