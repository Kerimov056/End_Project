using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Tag;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class TagService : ITagService
{
    private readonly ITagReadRepository _tagReadRepository;
    private readonly ITagWriteRepository _tagWriteRepository;
    private readonly IMapper _mapper;

    public TagService(ITagReadRepository tagReadRepository,
                      ITagWriteRepository tagWriteRepository,
                      IMapper mapper)
    {
        _tagReadRepository = tagReadRepository;
        _tagWriteRepository = tagWriteRepository;
        _mapper = mapper;
    }

    public async Task AddAsync(TagCreateDTO tagsCreateDTO)
    {
        Tags? tags = await _tagReadRepository
            .GetByIdAsyncExpression(x => x.Tag.ToLower().Equals(tagsCreateDTO.Tag));
        if (tags is not null) throw new DublicatedException("Dubilcated Tag Name!");

        var NewTag = _mapper.Map<Tags>(tagsCreateDTO);
        await _tagWriteRepository.AddAsync(NewTag);  //tagWrtireRepositoryde problem var exception verir
        await _tagWriteRepository.SavaChangeAsync();
    }

    public async Task<TagGetDTO> GetByIdAsync(Guid Id)
    {
        var ByTag = await _tagReadRepository.GetByIdAsync(Id);
        if (ByTag is null) throw new NotFoundException("Tag is Null");
        var MapByTag = _mapper.Map<TagGetDTO>(ByTag);
        return MapByTag;
    }

    public async Task<List<TagGetDTO>> GettAllAsync()
    {
        var Tags = await _tagReadRepository.GetAll().ToListAsync();
        var MapTags = _mapper.Map<List<TagGetDTO>>(Tags);
        return MapTags;
    }

    public async Task RemoveAsync(Guid Id)
    {
        var ByTag = await _tagReadRepository.GetByIdAsync(Id);
        if (ByTag is null) throw new NotFoundException("Tag is Null");
        _tagWriteRepository.Remove(ByTag);
        await _tagWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid Id, TagUpdateDTO tagsUpdateDTO)
    {
        var ByTag = await _tagReadRepository.GetByIdAsync(Id);
        if (ByTag is null) throw new NotFoundException("Tag is Null");
        _mapper.Map(tagsUpdateDTO,ByTag);
        _tagWriteRepository.Update(ByTag);
        await _tagWriteRepository.SavaChangeAsync();
    }
}
