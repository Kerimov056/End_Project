namespace EndProject.Application.DTOs.Car;

public class CarCampaignsDTO
{
    public bool isCampaigns { get; set; } = true;
    public DateTime PickUpCampaigns { get; set; }
    public DateTime ReturnCampaigns { get; set; }
    public double CampaignsInterest { get; set; }
    public string SuperAdminId { get; set; }
}
