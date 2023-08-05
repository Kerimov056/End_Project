using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Post;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EndProjet.Persistance.Implementations.Services;

public class PostService : IPostService
{
    private readonly IPostReadRepository _postReadRepository;
    private readonly IPostWriteRepository _postWriteRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<AppUser> _userManager;

    private readonly IMapper _mapper;

    public PostService(IPostReadRepository postReadRepository,
                       IPostWriteRepository postWriteRepository,
                       IHttpContextAccessor httpContextAccessor,
                       UserManager<AppUser> userManager,
                       IMapper mapper)
    {
        _postReadRepository = postReadRepository;
        _postWriteRepository = postWriteRepository;
        _contextAccessor = httpContextAccessor;
        _userManager = userManager;
        _mapper = mapper;
    }

    public Task AddAsync(PostCreateDTO postCreateDTO)
    {
        var ByUser = GetUserId();

        var NewPost = _mapper.Map<Posts>(postCreateDTO);

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
        var user = _contextAccessor.HttpContext.User;
        string userId = _userManager.GetUserId(user);
        return userId;
    }
}




