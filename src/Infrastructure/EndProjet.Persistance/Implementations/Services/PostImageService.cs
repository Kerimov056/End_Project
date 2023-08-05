using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Post;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;

namespace EndProjet.Persistance.Implementations.Services;

public class PostImageService : IPostImageService
{
    private readonly IPostImageWriteRepository _postImageWriteRepository;
    private readonly IPostReadRepository _postReadRepository;
    private readonly IMapper _mapper;

    public PostImageService(IPostImageWriteRepository postImageWriteRepository,
                            IPostImageReadRepository postImageReadRepository,
                            IPostReadRepository postReadRepository,
                            IStorageFile storageFile,
                            IMapper mapper)
    {
        _postImageWriteRepository = postImageWriteRepository;
        _postReadRepository = postReadRepository;
        _mapper = mapper;
    }

    public async Task AddAsync(PostImageCreateDTO postImageCreateDTO)
    {
        var PostImage = _mapper.Map<PostImage>(postImageCreateDTO);
        await _postImageWriteRepository.AddAsync(PostImage);
        await _postImageWriteRepository.SavaChangeAsync();
    }

    public async Task Update(Guid PostId, PostImageUpdateDTO postImageUpdateDTO)
    {
        var ByPost = await _postReadRepository.GetByIdAsync(PostId);
        if (ByPost is null) throw new NotFoundException("Post is Null");
        var PostImage = _mapper.Map<PostImage>(postImageUpdateDTO);
        _postImageWriteRepository.Update(PostImage);
        await _postImageWriteRepository.SavaChangeAsync();
    }
}
