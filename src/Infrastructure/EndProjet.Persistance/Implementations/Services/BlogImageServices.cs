using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.BlogImage;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class BlogImageServices : IBlogImageServices
{
    private readonly IBlogImageReadRepository _blogImageReadRepository;
    private readonly IBlogImageWriteRepository _blogImageWriteRepository;
    private readonly IStorageFile _storageFile;
    private readonly IMapper _mapper;

    public BlogImageServices(IBlogImageReadRepository blogImageReadRepository,
                             IBlogImageWriteRepository blogImageWriteRepository,
                             IMapper mapper,
                             IStorageFile storageFile)
    {
        _blogImageReadRepository = blogImageReadRepository;
        _blogImageWriteRepository = blogImageWriteRepository;
        _mapper = mapper;
        _storageFile = storageFile;
    }

    public async Task CreateAsync(BlogImageCreateDTO blogImageCreateDTO)
    {
        var ToEntity = _mapper.Map<BlogImage>(blogImageCreateDTO);
        if (blogImageCreateDTO.imagePath != null && blogImageCreateDTO.imagePath.Length > 0)
        {
            var ImagePath = await _storageFile.WriteFile("Upload\\Files", blogImageCreateDTO.imagePath);
            ToEntity.imagePath = ImagePath;
        }
        await _blogImageWriteRepository.AddAsync(ToEntity);
        await _blogImageWriteRepository.SavaChangeAsync();
    }

    public async Task<List<BlogImageGetDTO>> GetAllAsync()
    {
        var BlogImageAll = await _blogImageReadRepository.GetAll().ToListAsync();
        if (BlogImageAll is null) throw new NotFoundException("BlogImage is Null");

        var ToDto = _mapper.Map<List<BlogImageGetDTO>>(BlogImageAll);
        return ToDto;
    }

    public async Task<BlogImageGetDTO> GetByIdAsync(Guid Id)
    {
        var ByBlogImage = await _blogImageReadRepository.GetByIdAsync(Id);
        if (ByBlogImage is null) throw new NotFoundException("BlogImage is Null");

        var ToDto = _mapper.Map<BlogImageGetDTO>(ByBlogImage);
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
        _blogImageWriteRepository.Update(ByBlogImage);
        await _blogImageWriteRepository.SavaChangeAsync();
    }
}
