﻿using System.Text.Json.Serialization;

namespace EndProject.Application.DTOs.Auth.FacebookLogin;

public class FacebookUserAccessTokenValidation
{
    [JsonPropertyName("data")]
    public FacebookUserAccessTokenValidationData Data { get; set; }
}
public class FacebookUserAccessTokenValidationData
{
    [JsonPropertyName("is_valid")]
    public bool IsValid { get; set; }
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }
}