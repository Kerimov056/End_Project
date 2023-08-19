using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Chauffeurs;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class ChauffeursServices : IChauffeursServices
{
    private readonly IChauffeursReadRepository _chauffeursReadRepository;
    private readonly IChauffeursWriteRepository _chauffeursWriteRepository;
    private readonly IStorageFile _storageFile;
    private readonly IMapper _mapper;

    public ChauffeursServices(IChauffeursReadRepository chauffeursReadRepository,
                              IChauffeursWriteRepository chauffeursWriteRepository,
                              IMapper mapper,
                              IStorageFile storageFile)
    {
        _chauffeursReadRepository = chauffeursReadRepository;
        _chauffeursWriteRepository = chauffeursWriteRepository;
        _mapper = mapper;
        _storageFile = storageFile;
    }

    public async Task CreateAsync(ChauffeursCreateDTO chauffeursCreateDTO)
    {
        var newChauffeurs = _mapper.Map<Chauffeurs>(chauffeursCreateDTO);
        if (chauffeursCreateDTO.Image != null && chauffeursCreateDTO.Image.Length > 0)
        {
            var ImagePath = await _storageFile.WriteFile("Upload\\Files", chauffeursCreateDTO.Image);
            newChauffeurs.imagePath = ImagePath;
        }
        newChauffeurs.isChauffeurs = false;
        await _chauffeursWriteRepository.AddAsync(newChauffeurs);
        await _chauffeursWriteRepository.SavaChangeAsync();
    }

    public async Task<List<ChauffeursGetDTO>> GetAllAsync()
    {
        var allChauffeurs = await _chauffeursReadRepository.GetAll().ToListAsync();
        if (allChauffeurs is null) throw new NotFoundException("Chauffeurs is Null");

        var chauffeurss = _mapper.Map<List<ChauffeursGetDTO>>(allChauffeurs);
        return chauffeurss;
    }

    public async Task<ChauffeursGetDTO> GetByIdAsync(Guid Id)
    {
        var byChauffeurs = await _chauffeursReadRepository.GetByIdAsync(Id);
        if (byChauffeurs is null) throw new NotFoundException("Chauffeur is Null");

        var chauffeurs = _mapper.Map<ChauffeursGetDTO>(byChauffeurs);
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
        if (chauffeursUpdateDTO.Image != null && chauffeursUpdateDTO.Image.Length > 0)
        {
            var ImagePath = await _storageFile.WriteFile("Upload\\Files", chauffeursUpdateDTO.Image);
            byChauffeurs.imagePath = ImagePath;
        }

        _chauffeursWriteRepository.Update(byChauffeurs);
        await _chauffeursWriteRepository.SavaChangeAsync();
    }

    public async Task IsChauffeurs(Guid? cheuffeursId)
    {
        var byChauffeurs = await _chauffeursReadRepository.GetByIdAsync((Guid)cheuffeursId);
        if (byChauffeurs is null) throw new NotFoundException("Chauffeur is Null");

        byChauffeurs.isChauffeurs = true;
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
        return chauffeurss;
    }
}
