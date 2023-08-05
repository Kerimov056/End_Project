using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.NewTag;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.Implementations.Services;

public class NewTagService : INewTagService
{
    private readonly INewTagReadRepository _newTagReadRepository;
    private readonly INewTagWriteRepository _newTagWriteRepository;
    private readonly ITagService _tagService;
    private readonly IMapper _mapper;

    public NewTagService(INewTagReadRepository newTagReadRepository,
                         INewTagWriteRepository newTagWriteRepository,
                         ITagService tagService,
                         IMapper mapper)
    {
        _newTagReadRepository = newTagReadRepository;
        _newTagWriteRepository = newTagWriteRepository;
        _tagService = tagService;
        _mapper = mapper;
    }

    public async Task AddAsync(NewTagCreateDTO newTagCreateDTO)
    {
        foreach (var item in await _tagService.GettAllAsync())
        {
            if (newTagCreateDTO.Tag==item.Tag)
            {
                var tag = _mapper.Map<NewTag>(newTagCreateDTO);
                tag.Tag = item.Tag;
                await _newTagWriteRepository.AddAsync(tag);
                await _newTagWriteRepository.SavaChangeAsync();
            }
        }

        var newTag = _mapper.Map<NewTag>(newTagCreateDTO);
        await _newTagWriteRepository.AddAsync(newTag);
        await _newTagWriteRepository.SavaChangeAsync();
    }

}
