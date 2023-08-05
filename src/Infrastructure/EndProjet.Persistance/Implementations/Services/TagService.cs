using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Tag;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;

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
        var NewTag = _mapper.Map<Tags>(tagsCreateDTO);
        await _tagWriteRepository.AddAsync(NewTag);
        await _tagWriteRepository.SavaChangeAsync();
    }

    public async Task<TagGetDTO> GetByIdAsync(Guid Id)
    {
        var ByTag = await _tagReadRepository.GetByIdAsync(Id);
        if (ByTag is null) throw new NotFoundException("Tag is Null");
        var MapByTag = _mapper.Map<TagGetDTO>(ByTag);
        return MapByTag;
    }

    public Task<List<TagGetDTO>> GettAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid Id, TagUpdateDTO tagsUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
