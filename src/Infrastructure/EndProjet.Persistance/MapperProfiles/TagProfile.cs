using AutoMapper;
using EndProject.Application.DTOs.Tag;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class TagProfile:Profile
{
	public TagProfile()
	{
		CreateMap<Tags, TagGetDTO>().ReverseMap();
		CreateMap<Tags, TagCreateDTO>().ReverseMap();
		CreateMap<Tags, TagUpdateDTO>().ReverseMap();
	}
}
