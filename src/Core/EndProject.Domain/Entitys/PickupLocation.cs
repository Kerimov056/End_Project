﻿using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class PickupLocation:BaseEntity
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Guid CarReservationId { get; set; }
    public CarReservation CarReservation { get; set; }
}
