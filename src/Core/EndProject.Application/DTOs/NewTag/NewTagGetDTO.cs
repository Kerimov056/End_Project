﻿namespace EndProject.Application.DTOs.NewTag;

public class NewTagGetDTO
{
    public Guid Id { get; set; }
    public string Tag { get; set; }
    public Guid PostsId { get; set; }
}
