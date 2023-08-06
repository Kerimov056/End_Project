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
    private readonly IStorageFile _storgeFile;
    private readonly IMapper _mapper;

    public PostImageService(IPostImageWriteRepository postImageWriteRepository,
                            IPostImageReadRepository postImageReadRepository,
                            IPostReadRepository postReadRepository,
                            IStorageFile storageFile,
                            IMapper mapper)
    {
        _postImageWriteRepository = postImageWriteRepository;
        _postReadRepository = postReadRepository;
        _storgeFile = storageFile;
        _mapper = mapper;
    }

    public async Task AddAsync(Guid PostId,PostImageCreateDTO postImageCreateDTO)
    {
        PostImage PostImage = _mapper.Map<PostImage>(postImageCreateDTO);
        PostImage.PostsId = PostId;
        //if (PostImage.imagePath != null && postImageCreateDTO.ImagePath.Length > 0)
        //{
        //    var ImagePath = await _storgeFile.WriteFile("Upload\\Files", postImageCreateDTO.ImagePath);
        //    PostImage.imagePath = ImagePath;
        //}

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
