﻿using EndProject.Application.DTOs.BlogImage;
using EndProject.Domain.Entitys;

namespace EndProject.Application.DTOs.Blog;

public class BlogGetDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<BlogImageGetDTO> BlogImageGetDTOs { get; set; }
}
