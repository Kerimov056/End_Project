namespace EndProject.Application.DTOs.Post_Tag;

public class Post_TagGetDTO
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }    
    public Guid TagId { get; set; }
}
