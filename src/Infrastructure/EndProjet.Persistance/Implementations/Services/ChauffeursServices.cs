using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Chauffeurs;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Common;
using EndProjet.Persistance.Exceptions;
using EndProjet.Persistance.ExtensionsMethods;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class ChauffeursServices : IChauffeursServices
{
    private readonly IChauffeursReadRepository _chauffeursReadRepository;
    private readonly IChauffeursWriteRepository _chauffeursWriteRepository;
    private readonly IMapper _mapper;

    public ChauffeursServices(IChauffeursReadRepository chauffeursReadRepository,
                              IChauffeursWriteRepository chauffeursWriteRepository,
                              IMapper mapper)
    {
        _chauffeursReadRepository = chauffeursReadRepository;
        _chauffeursWriteRepository = chauffeursWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(ChauffeursCreateDTO chauffeursCreateDTO)
    {
        var newChauffeurs = _mapper.Map<Chauffeurs>(chauffeursCreateDTO);
        if (chauffeursCreateDTO.Image is not null)
        {
            newChauffeurs.imagePath = await chauffeursCreateDTO.Image.GetBytes();
        }
        newChauffeurs.isChauffeurs = false;
        await _chauffeursWriteRepository.AddAsync(newChauffeurs);
        await _chauffeursWriteRepository.SavaChangeAsync();
    }

    public async Task<List<ChauffeursGetDTO>> GetAllAsync()
    {
        var allChauffeurs = await _chauffeursReadRepository
            .GetAll()
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync();
        if (allChauffeurs is null) throw new NotFoundException("Chauffeurs is Null");

        var chauffeurss = _mapper.Map<List<ChauffeursGetDTO>>(allChauffeurs);


        foreach (var item in chauffeurss)
        {
            Chauffeurs chauffeurs = allChauffeurs.FirstOrDefault(x => x.Id == item.Id)
                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

            List<string> images = new();
            images.Add(Convert.ToBase64String(chauffeurs.imagePath));
            item.ImagePath = images[0];
        }
        return chauffeurss;
    }

    public async Task<ChauffeursGetDTO> GetByIdAsync(Guid Id)
    {
        var byChauffeurs = await _chauffeursReadRepository.GetByIdAsync(Id);
        if (byChauffeurs is null) throw new NotFoundException("Chauffeur is Null");
        var chauffeurs = _mapper.Map<ChauffeursGetDTO>(byChauffeurs);
        chauffeurs.ImagePath = Convert.ToBase64String(byChauffeurs.imagePath);
        return chauffeurs;
    }

    public async Task RemoveAsync(Guid id)
    {
        var byChauffeurs = await _chauffeursReadRepository.GetByIdAsync(id);
        if (byChauffeurs is null) throw new NotFoundException("Chauffeur is Null");

        _chauffeursWriteRepository.Remove(byChauffeurs);
        await _chauffeursWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid id, ChauffeursUpdateDTO chauffeursUpdateDTO)
    {
        var byChauffeurs = await _chauffeursReadRepository.GetByIdAsync(id);
        if (byChauffeurs is null) throw new NotFoundException("Chauffeur is Null");
        _mapper.Map(chauffeursUpdateDTO, byChauffeurs);

        if (chauffeursUpdateDTO.Image is not null) byChauffeurs.imagePath = await chauffeursUpdateDTO.Image.GetBytes();
        _chauffeursWriteRepository.Update(byChauffeurs);
        await _chauffeursWriteRepository.SavaChangeAsync();

    }

    public async Task IsChauffeursTrue(Guid? cheuffeursId)
    {
        var byChauffeurs = await _chauffeursReadRepository.GetByIdAsync((Guid)cheuffeursId);
        if (byChauffeurs is null) throw new NotFoundException("Chauffeur is Null");

        byChauffeurs.isChauffeurs = true;
        _chauffeursWriteRepository.Update(byChauffeurs);
        await _chauffeursWriteRepository.SavaChangeAsync();
    }


    public async Task IsChauffeursFalse(Guid? cheuffeursId)
    {
        var byChauffeurs = await _chauffeursReadRepository.GetByIdAsync((Guid)cheuffeursId);
        if (byChauffeurs is null) throw new NotFoundException("Chauffeur is Null");

        byChauffeurs.isChauffeurs = false;
        _chauffeursWriteRepository.Update(byChauffeurs);
        await _chauffeursWriteRepository.SavaChangeAsync();
    }

    public async Task<List<ChauffeursGetDTO>> ViewGetAll()
    {
        var allChauffeurs = await _chauffeursReadRepository
                    .GetAll()
                    .Where(x => x.isChauffeurs == false)
                    .ToListAsync();
        if (allChauffeurs is null) throw new NotFoundException("Chauffeurs is Null");

        var chauffeurss = _mapper.Map<List<ChauffeursGetDTO>>(allChauffeurs);
        foreach (var item in chauffeurss)
        {
            Chauffeurs chauffeurs = allChauffeurs.FirstOrDefault(x => x.Id == item.Id)
                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

            List<string> images = new();
            images.Add(Convert.ToBase64String(chauffeurs.imagePath));
            item.ImagePath = images[0];
        }
        return chauffeurss;
    }

}
