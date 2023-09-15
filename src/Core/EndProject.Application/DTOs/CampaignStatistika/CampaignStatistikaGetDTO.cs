﻿using EndProject.Application.DTOs.CarReservation;

namespace EndProject.Application.DTOs.CampaignStatistika;

public class CampaignStatistikaGetDTO
{
    public Guid Id { get; set; }
    public int ReservationSum { get; set; }
    public string NameCampaignName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime FinshTime { get; set; }
    public List<CarReservationGetDTO>? Reservation { get; set; }
}
