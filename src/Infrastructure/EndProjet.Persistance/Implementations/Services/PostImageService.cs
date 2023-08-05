using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Post;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.Implementations.Services;

public class PostImageService : IPostImageService
{
    private readonly IPostImageWriteRepository _postImageWriteRepository;
    private readonly IPostImageReadRepository _postImageReadRepository;
    private readonly IPostReadRepository _postReadRepository;
    private readonly IStorageFile _storageFile;
    private readonly IMapper _mapper;

    public PostImageService(IPostImageWriteRepository postImageWriteRepository,
                            IPostImageReadRepository postImageReadRepository,
                            IPostReadRepository postReadRepository,
                            IStorageFile storageFile,
                            IMapper mapper)
    {
        _postImageWriteRepository = postImageWriteRepository;
        _postImageReadRepository = postImageReadRepository;
        _postReadRepository = postReadRepository;
        _storageFile = storageFile;
        _mapper = mapper;
    }

    public async Task AddAsync(PostImageCreateDTO postImageCreateDTO)
    {
        var PostImage = _mapper.Map<PostImage>(postImageCreateDTO);
        await _postImageWriteRepository.AddAsync(PostImage);
        await _postImageWriteRepository.SavaChangeAsync();
    }

    public Task Update(Guid PostId, PostImageUpdateDTO postImageUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
