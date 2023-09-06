namespace EndProject.Application.DTOs.CarComment;

public class CarCommentCreateDTO
{
    public string comment { get; set; }
    public string UserName { get; set; }
    public Guid carid { get; set; }
    public Guid appuserid { get; set; }
}
