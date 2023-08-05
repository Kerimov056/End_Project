using AutoMapper;
using EndProject.Application.DTOs.Like;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class LikeProfile:Profile
{
	public LikeProfile()
	{
		CreateMap<Like, LikeGetDTO>().ReverseMap();
		CreateMap<Like, LikeCreateDTO>().ReverseMap();
		CreateMap<Like, LikeUpdateDTO>().ReverseMap();
	}
}
