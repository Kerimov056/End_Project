using EndProject.Application.DTOs.CarComment;
using EndProject.Application.DTOs.CarImage;
using EndProject.Application.DTOs.CarType;
using EndProject.Application.DTOs.Category;
using EndProject.Domain.Enums.CampaignsStatus;

namespace EndProject.Application.DTOs.Car;

public class GameCarGetDTO
{
    public Guid Id { get; set; }
    public string Marka { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string Description { get; set; }
    public bool isReserv { get; set; }
    public string? CampaignName { get; set; }
    public decimal? CampaignsPrice { get; set; }
    public double? CampaignsInterest { get; set; }
    public CampaignsStatus Status { get; set; }
    public bool isCampaigns { get; set; }
    public DateTime? PickUpCampaigns { get; set; }
    public DateTime? ReturnCampaigns { get; set; }
    public CarTypeGetDTO CarType { get; set; }
    public CarCategoryGetDTO CarCategory { get; set; }
    public List<CarCommentGetDTO> carCommentGetDTO { get; set; }
    public List<CarImageGetDTO> CarImages { get; set; }
    public List<string> CarTags { get; set; }
    public Guid? ReservationsId { get; set; }
    public string? ImageSrc { get; set; }
}
