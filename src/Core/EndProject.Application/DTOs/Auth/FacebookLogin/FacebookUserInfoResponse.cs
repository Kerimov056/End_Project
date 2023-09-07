using System.Text.Json.Serialization;

namespace EndProject.Application.DTOs.Auth.FacebookLogin;

public class FacebookUserInfoResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}