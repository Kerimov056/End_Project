namespace EndProject.Application.DTOs.BlogImage;

public class BlogImageGetDTO
{
    public Guid Id { get; set; }
    public string imagePath { get; set; }
    public Guid BlogId { get; set; }
}
