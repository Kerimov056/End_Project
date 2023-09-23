using Microsoft.AspNetCore.Mvc;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    [Route("/proxy/googlemaps")]
    [HttpGet]
    public async Task<IActionResult> GetGoogleMapsData(string placeId)
    {
        using (HttpClient client = new HttpClient())
        {
            string apiKey = "AIzaSyCs2aJhlTdKjfTiQZ5kNP2-3QMNzPuLf7o";
            string url = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&fields=name,photos&key={apiKey}";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                return Ok(responseData);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}
