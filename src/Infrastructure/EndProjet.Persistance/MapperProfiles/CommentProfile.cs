using AutoMapper;
using EndProject.Application.DTOs.Comments;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class CommentProfile:Profile
{
	public CommentProfile()
	{
		CreateMap<Comments,CommentGetDTO>().ReverseMap();
		CreateMap<Comments,CommentCreateDTO>().ReverseMap();
	}
}
