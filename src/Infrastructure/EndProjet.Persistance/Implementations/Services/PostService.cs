using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.NewTag;
using EndProject.Application.DTOs.Post;
using EndProject.Application.DTOs.Tag;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Identity;
using EndProjet.Persistance.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EndProjet.Persistance.Implementations.Services;

public class PostService : IPostService
{
    private readonly IPostReadRepository _postReadRepository;
    private readonly IPostWriteRepository _postWriteRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<AppUser> _userManager;
    private readonly INewTagService _newTagService;
    private readonly IPostImageService _postImageService;
    private readonly AppDbContext _appDbContext;
    private readonly ITagService _tagService;
    private readonly IMapper _mapper;

    public PostService(IPostReadRepository postReadRepository,
                       IPostWriteRepository postWriteRepository,
                       IHttpContextAccessor httpContextAccessor,
                       IPostImageService postImageService,
                       ITagService tagService,
                       INewTagService newTagService,
                       AppDbContext appDbContext,
                       UserManager<AppUser> userManager,
                       IMapper mapper)
    {
        _postReadRepository = postReadRepository;
        _postWriteRepository = postWriteRepository;
        _contextAccessor = httpContextAccessor;
        _postImageService = postImageService;
        _tagService = tagService;
        _newTagService= newTagService;
        _appDbContext = appDbContext;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task AddAsync(PostCreateDTO postCreateDTO)
    {
        var ByUser = GetUserId();
        var NewPost = _mapper.Map<Posts>(postCreateDTO);
        NewPost.AppUserId = ByUser;

        await _postWriteRepository.AddAsync(NewPost); 
        await _postWriteRepository.SavaChangeAsync();

        //var NewPostImageList = new List<PostImageCreateDTO>();
        foreach (var imageDto in postCreateDTO.Images)
        {
            var newPostImage = _mapper.Map<PostImageCreateDTO>(imageDto);
            await _postImageService.AddAsync(NewPost.Id, newPostImage);
        }


        //foreach (var tag in postCreateDTO.NewTagCreateDTOs)
        //{
        //    TagCreateDTO tagCreateDTO = new()
        //    {
        //        Tag = tag.Tag
        //    };
        //    await _tagService.AddAsync(tagCreateDTO);
        //}


        var tags = new List<NewTagCreateDTO>();
        foreach (var item in tags)
        {
            var tagCreateDto = new TagCreateDTO
            {
                Tag = item.Tag
            };
            await _tagService.AddAsync(tagCreateDto);
        }
        
        foreach (var item in await _tagService.GettAllAsync())
        {
            var Post_Tag = new Post_Tag
            {
                TagsId = item.Id,
                PostsId = NewPost.Id
            };
            await _appDbContext.Post_Tags.AddAsync(Post_Tag);
        }
        await _postWriteRepository.SavaChangeAsync();
    }

    public Task<PostGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PostGetDTO>> GettAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid Id, PostUpdateDTO postUpdateDTO)
    {
        throw new NotImplementedException();
    }


    private string GetUserId()
    {
        //var user = _contextAccessor.HttpContext.User;
        string userId = "8a244e85-22bd-42a2-aa8e-0d5f93d9bdb4";
        return userId;
    }
}




