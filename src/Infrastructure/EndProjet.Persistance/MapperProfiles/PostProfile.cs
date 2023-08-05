using AutoMapper;
using EndProject.Application.DTOs.Post;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class PostProfile:Profile
{
	public PostProfile()
	{
		CreateMap<Posts, PostGetDTO>().ReverseMap();
		CreateMap<PostLike, PostLikeGetDTO>().ReverseMap();
		CreateMap<PostLike, PostLikeCreateDTO>().ReverseMap();
		CreateMap<PostImage, PostImageGetDTO>().ReverseMap();
		CreateMap<PostImage, PostImageCreateDTO>().ReverseMap();
		CreateMap<PostImage, PostImageUpdateDTO>().ReverseMap();
	}
}
