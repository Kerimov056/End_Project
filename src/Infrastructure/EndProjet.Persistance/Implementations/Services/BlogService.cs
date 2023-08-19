using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Blog;
using EndProject.Application.DTOs.BlogImage;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.Implementations.Services;

public class BlogService : IBlogService
{
    private readonly IBlogReadRepository _blogReadRepository;
    private readonly IBlogWriteRepository _blogWriteRepository;
    private readonly IMapper _mapper;
    private readonly IBlogImageServices _blogImageServices;

    public BlogService(IBlogReadRepository blogReadRepository,
                       IBlogWriteRepository blogWriteRepository,
                       IMapper mapper,
                       IBlogImageServices blogImageServices)
    {
        _blogReadRepository = blogReadRepository;
        _blogWriteRepository = blogWriteRepository;
        _mapper = mapper;
        _blogImageServices = blogImageServices;
    }

    public async Task CreateAsync(BlogCreateDTO blogCreateDTO)
    {
        var newBlog = _mapper.Map<Blog>(blogCreateDTO);
        await _blogWriteRepository.AddAsync(newBlog);
        await _blogWriteRepository.SavaChangeAsync();

        if (newBlog.BlogImages is not null)
        {
            foreach (var item in blogCreateDTO.BlogImageCreateDTOs)
            {
                var newBlogImage = new BlogImageCreateDTO
                {
                    BlogId = newBlog.Id,
                    imagePath = item.imagePath
                };
                await _blogImageServices.CreateAsync(newBlogImage);
            }
        }
    }

    public Task<List<BlogGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BlogGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, BlogUpdateDTO blogUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
