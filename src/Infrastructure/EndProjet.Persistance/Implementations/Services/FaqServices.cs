using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Advantage;
using EndProject.Application.DTOs.Faq;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.Implementations.Services;

public class FaqServices : IFaqServices
{
    private readonly IFaqReadRepository _faqReadRepository;
    private readonly IFaqWriteRepository _faqWriteRepository;
    private readonly IMapper _mapper;

    public FaqServices(IFaqReadRepository faqReadRepository,
                       IFaqWriteRepository faqWriteRepository,
                       IMapper mapper)
    {
        _faqReadRepository = faqReadRepository;
        _faqWriteRepository = faqWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(FaqCreateDTO faqCreateDTO)
    {
        var ToEntity = _mapper.Map<Faq>(faqCreateDTO);
        await _faqWriteRepository.AddAsync(ToEntity);
        await _faqWriteRepository.SavaChangeAsync();
    }

    public Task<List<FaqGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<FaqGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, FaqUpdateDTO faqUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
