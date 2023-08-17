using AutoMapper;
using EndProject.Application.DTOs.CarComment;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class CarCommentProfile:Profile
{
    public CarCommentProfile()
    {
        CreateMap<CarComment, CarCommentGetDTO>().ReverseMap();
        CreateMap<CarComment, CarCommentCreateDTO>().ReverseMap();
        CreateMap<CarComment, CarCommentUpdateDTO>().ReverseMap();
    }
}
