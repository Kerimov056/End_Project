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
        var newBlog = new Blog
        {
            Title = blogCreateDTO.Title,
            Description = blogCreateDTO.Description
        };

        if (newBlog.BlogImages is not null)
        {
            foreach (var item in blogCreateDTO.blogImages)
            {
                var newBlogImage = new BlogImageCreateDTO
                {
                    BlogId = newBlog.Id,
                    imagePath = item
                };
                await _blogImageServices.CreateAsync(newBlogImage);
            }
        }

        await _blogWriteRepository.AddAsync(newBlog);
        await _blogWriteRepository.SavaChangeAsync();
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
