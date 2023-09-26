﻿using EndProject.Domain.Enums.Role;

namespace EndProject.Application.DTOs.ShareTrip;

public class ShareTripCreateDTO
{
    public string Email { get; set; }
    public string? Message { get; set; }
    public Guid TripId { get; set; }
    public TripRole TripRole { get; set; }
    public string? AppUserId { get; set; }

}
