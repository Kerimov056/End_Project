using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Enums.CampaignsStatus;

namespace EndProject.Domain.Entitys;

public class Car : BaseEntity
{
    public string Marka { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string Description { get; set; }
    public bool isReserv { get; set; } = false;
    public bool isCampaigns { get; set; } = false;
    public string? CampaignName { get; set; }
    public decimal? CampaignsPrice { get; set; }
    public decimal? CampaignsInterest { get; set; }
    public DateTime? PickUpCampaigns { get; set; }
    public DateTime? ReturnCampaigns { get; set; }
    public CampaignsStatus Status { get; set; }
    public CarType carType { get; set; }
    public CarCategory carCategory { get; set; }
    public List<CarImage>? carImages { get; set; }
    public List<CarTag>? carTags { get; set; }
    public List<CarComment>? Comments { get; set; }
    public List<CarReservation>? Reservations { get; set; }
    public List<OtherCarReservation>? OtherReservations { get; set; }
    //public JPSData? JPSData { get; set; }

}
