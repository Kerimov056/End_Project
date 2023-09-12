using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.Abstraction.Services.Stroge;
using EndProject.Application.DTOs.BlogImage;
using EndProject.Application.DTOs.Slider;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Common;
using EndProjet.Persistance.Exceptions;
using EndProjet.Persistance.ExtensionsMethods;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class BlogImageServices : IBlogImageServices
{
    private readonly IBlogImageReadRepository _blogImageReadRepository;
    private readonly IBlogImageWriteRepository _blogImageWriteRepository;
    private readonly IStorageFile _storageFile;
    private readonly IBlogReadRepository _blogReadRepository;
    private readonly IMapper _mapper;

    public BlogImageServices(IBlogImageReadRepository blogImageReadRepository,
                             IBlogImageWriteRepository blogImageWriteRepository,
                             IMapper mapper,
                             IStorageFile storageFile,
                             IBlogReadRepository blogReadRepository)
    {
        _blogImageReadRepository = blogImageReadRepository;
        _blogImageWriteRepository = blogImageWriteRepository;
        _mapper = mapper;
        _storageFile = storageFile;
        _blogReadRepository = blogReadRepository;
    }

    public async Task CreateAsync(BlogImageCreateDTO blogImageCreateDTO)
    {
        BlogImage ToEntity = new()
        {
            BlogId = blogImageCreateDTO.BlogId,
        };
        if (blogImageCreateDTO.imagePath is not null) 
            ToEntity.imagePath = await blogImageCreateDTO.imagePath.GetBytes();

        await _blogImageWriteRepository.AddAsync(ToEntity);
        await _blogImageWriteRepository.SavaChangeAsync();
    }

    public async Task<List<BlogImageGetDTO>> GetAllAsync()
    {
        var BlogImageAll = await _blogImageReadRepository.GetAll().ToListAsync();
        if (BlogImageAll is null) throw new NotFoundException("BlogImage is Null");

        var ToDto = _mapper.Map<List<BlogImageGetDTO>>(BlogImageAll);
        foreach (var item in ToDto)
        {
            BlogImage blogImage = BlogImageAll.FirstOrDefault(x => x.Id == item.Id)
                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

            List<string> images = new();
            images.Add(Convert.ToBase64String(blogImage.imagePath));
            item.imagePath = images[0];
        }
        return ToDto;
    }

    public async Task<List<BlogImageGetDTO>> GetAllBlogIdAsync(Guid blogId)
    {
        var byBlog = await _blogReadRepository.GetByIdAsync(blogId);
        if (byBlog is null) throw new NotFoundException("Blog Is Null");

        var BlogImageAll = await _blogImageReadRepository
                            .GetAll()
                            .Include(x=>x.Blog)
                            .Where(x=>x.BlogId == blogId)
                            .ToListAsync();

        var blogImageDto = _mapper.Map<List<BlogImageGetDTO>>(BlogImageAll);
        foreach (var item in blogImageDto)
        {
            BlogImage blogImage = BlogImageAll.FirstOrDefault(x => x.Id == item.Id)
                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

            List<string> images = new();
            images.Add(Convert.ToBase64String(blogImage.imagePath));
            item.imagePath = images[0];
        }
        return blogImageDto;
    }

    public async Task<BlogImageGetDTO> GetByIdAsync(Guid Id)
    {
        var ByBlogImage = await _blogImageReadRepository.GetByIdAsync(Id);
        if (ByBlogImage is null) throw new NotFoundException("BlogImage is Null");
        var ToDto = _mapper.Map<BlogImageGetDTO>(ByBlogImage);
        ToDto.imagePath = Convert.ToBase64String(ByBlogImage.imagePath);
        return ToDto;
    }

    public async Task RemoveAsync(Guid id)
    {
        var ByBlogImage = await _blogImageReadRepository.GetByIdAsync(id);
        if (ByBlogImage is null) throw new NotFoundException("BlogImage is Null");

        _blogImageWriteRepository.Remove(ByBlogImage);
        await _blogImageWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid id, BlogImageUpdateDTO blogImageUpdateDTO)
    {
        var ByBlogImage = await _blogImageReadRepository.GetByIdAsync(id);
        if (ByBlogImage is null) throw new NotFoundException("BlogImage is Null");
        _mapper.Map(blogImageUpdateDTO, ByBlogImage);

        if (blogImageUpdateDTO.imagePath is not null) ByBlogImage.imagePath = await blogImageUpdateDTO.imagePath.GetBytes();
        _blogImageWriteRepository.Update(ByBlogImage);
        await _blogImageWriteRepository.SavaChangeAsync();
    }
}
