namespace EndProject.Application.DTOs.Like;

public class LikeGetDTO
{
    public Guid Id { get; set; }
    public int LikeSum { get; set; }
    public string AppUserId { get; set; }
    public Guid CarComentId { get; set; }
}
