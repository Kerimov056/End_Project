﻿using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.Chauffeurs;

public class ChauffeursUpdateDTO
{
    public string Name { get; set; }
    public string Number { get; set; }
    public IFormFile Image { get; set; }
    public double Price { get; set; }
    public bool isChauffeurs { get; set; }
}
