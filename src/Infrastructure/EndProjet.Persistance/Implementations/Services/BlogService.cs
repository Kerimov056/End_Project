using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Blog;
using EndProject.Application.DTOs.BlogImage;
using EndProject.Application.DTOs.Car;
using EndProject.Application.DTOs.CarImage;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

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

        await _blogWriteRepository.AddAsync(newBlog);
        await _blogWriteRepository.SavaChangeAsync();

        if (blogCreateDTO.blogImages is not null)
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
    }

    public async Task<List<BlogGetDTO>> GetAllAsync()
    {
        var allBlog = await _blogReadRepository
            .GetAll()
            .Include(x => x.BlogImages)
            .ToListAsync();
        if (allBlog is null) throw new NotFoundException("Blog is Null");

        var ToDto = _mapper.Map<List<BlogGetDTO>>(allBlog);
        return ToDto;
    }

    public async Task<BlogGetDTO> GetByIdAsync(Guid Id)
    {
        var ByBlog = await _blogReadRepository
            .GetAll()
            .Include(x => x.BlogImages)
            .FirstOrDefaultAsync(x => x.Id == Id);
        if (ByBlog is null) throw new NotFoundException("Blog is Null");

        var ToDto = _mapper.Map<BlogGetDTO>(ByBlog);
        return ToDto;

    }

    public async Task RemoveAsync(Guid id)
    {
        var ByBlog = await _blogReadRepository.GetByIdAsync(id);
        if (ByBlog is null) throw new NotFoundException("Blog is Null");

        _blogWriteRepository.Remove(ByBlog);
        await _blogWriteRepository.SavaChangeAsync();   
    }

    public async Task UpdateAsync(Guid id, BlogUpdateDTO blogUpdateDTO)
    {
        var ByBlog = await _blogReadRepository.GetByIdAsync(id);
        if (ByBlog is null) throw new NotFoundException("Blog is Null");
            
        ByBlog.Title = blogUpdateDTO.Title;
        ByBlog.Description = blogUpdateDTO.Description;

        if (blogUpdateDTO.blogImages is not null)
        {
            foreach (var item in blogUpdateDTO.blogImages)
            {
                if (ByBlog.BlogImages is null)
                {
                    var carImageCreateDto = new BlogImageCreateDTO
                    {
                        BlogId = ByBlog.Id,
                        imagePath = item
                    };
                    await _blogImageServices.CreateAsync(carImageCreateDto);
                }
            }
        }

        _blogWriteRepository.Update(ByBlog);
        await _blogWriteRepository.SavaChangeAsync();
    }
}
