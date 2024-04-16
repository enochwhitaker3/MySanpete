using RazorClassLibrary.Data;
using RazorClassLibrary.Services;

namespace MySanpeteWeb.Services;

public class GoogleApiService(IConfiguration config) : IGoogleApiService
{
    public async Task<Coords?> GetCoordsOfAddress(string address)
    {
        var client = new HttpClient();
        var response = await client.GetFromJsonAsync<Address>($"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={config.GetValue<string>("Google_Api")}");
        if(response is not null)
        {
            return new Coords() {X = response.results.First().geometry.location.lat, Y = response.results.First().geometry.location.lng };
        }
        return null;
    }
}

