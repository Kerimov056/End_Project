using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Advantage;
using EndProject.Application.DTOs.Faq;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<FaqGetDTO>> GetAllAsync()
    {
        var AllFaq = await _faqReadRepository.GetAll().ToListAsync();
        if (AllFaq is null) throw new NotFoundException("Faq is Null");

        var ToDto = _mapper.Map<List<FaqGetDTO>>(AllFaq);
        return ToDto;
    }

    public async Task<FaqGetDTO> GetByIdAsync(Guid Id)
    {
        var byFaq = await _faqReadRepository.GetByIdAsync(Id);
        if (byFaq is null) throw new NotFoundException("Advantage is Null");

        var ToDto = _mapper.Map<FaqGetDTO>(byFaq);
        return ToDto;
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
