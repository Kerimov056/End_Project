﻿using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class Chauffeurs:BaseEntity
{
    public string Name { get; set; }
    public string Number { get; set; }
    public byte[] imagePath { get; set; }
    public double Price { get; set; }
    public bool isChauffeurs { get; set; } = false;
    public CarReservation? CarReservation { get; set; }
}
