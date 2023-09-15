﻿namespace EndProject.Application.DTOs.Car;

public class CarCampaignsDTO
{
    public bool isCampaigns { get; set; } = true;
    public string? CampaignName { get; set; }
    public DateTime PickUpCampaigns { get; set; }
    public DateTime ReturnCampaigns { get; set; }
    public decimal CampaignsInterest { get; set; }
    public string SuperAdminId { get; set; }
}
