﻿using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class CarImage:BaseEntity
{
    public string imagePath { get; set; }
    public Guid CarId { get; set; }
    public Car Car { get; set; }
}
