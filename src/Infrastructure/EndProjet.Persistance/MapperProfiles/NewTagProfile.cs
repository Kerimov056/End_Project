using AutoMapper;
using EndProject.Application.DTOs.NewTag;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class NewTagProfile:Profile
{
	public NewTagProfile()
	{
		CreateMap<NewTag, NewTagGetDTO>().ReverseMap();
		CreateMap<NewTag, NewTagCreateDTO>().ReverseMap();
		CreateMap<NewTag, NewTagUpdateDTO>().ReverseMap();
	}
}
