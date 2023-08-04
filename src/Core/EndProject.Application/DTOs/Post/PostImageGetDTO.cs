namespace EndProject.Application.DTOs.Post;

public class PostImageGetDTO
{
    public Guid Id { get; set; }
    public string ImagePath { get; set; }
    public Guid PostsId { get; set; }
}
