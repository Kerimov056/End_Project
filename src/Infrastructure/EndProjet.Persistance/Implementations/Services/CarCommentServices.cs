using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarComment;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class CarCommentServices : ICarCommentServices
{
    private readonly ICarCommentReadRepository _carCommentReadRepository;
    private readonly ICarCommentWriteRepository _carCommentWriteRepository;
    private readonly ICarReadRepository _carReadRepository;
    private readonly IMapper _mapper;

    public CarCommentServices(ICarCommentReadRepository carCommentReadRepository,
                              ICarCommentWriteRepository carCommentWriteRepository,
                              IMapper mapper)
    {
        _carCommentReadRepository = carCommentReadRepository;
        _carCommentWriteRepository = carCommentWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CarCommentCreateDTO carCommentCreateDTO)
    {
        var newCarCommnet = _mapper.Map<CarComment>(carCommentCreateDTO);
        await _carCommentWriteRepository.AddAsync(newCarCommnet);
        await _carCommentWriteRepository.SavaChangeAsync();
    }

    public async Task<List<CarCommentGetDTO>> GetAllAsync(Guid CarId)
    {
        //var ByCar = await _carReadRepository.GetByIdAsync(CarId);
        //if (ByCar is null) throw new NotFoundException("Car is Null");

        var CarAllComment = await _carCommentReadRepository
            .GetAll()
            .Include(x => x.CarId == CarId)
            .ToListAsync();
        if (CarAllComment is null) throw new NotFoundException("Comment is null");

        var ToDto = _mapper.Map<List<CarCommentGetDTO>>(CarAllComment);
        return ToDto;
    }

    public async Task<CarCommentGetDTO> GetByIdAsync(Guid Id)
    {
        var ByComment = await _carCommentReadRepository.GetByIdAsync(Id);
        if (ByComment is null) throw new NotFoundException("Comment is Null");

        var ToDto = _mapper.Map<CarCommentGetDTO>(ByComment);
        return ToDto;
    }

    public async Task RemoveAsync(Guid id)
    {
        var ByComment = await _carCommentReadRepository.GetByIdAsync(id);
        if (ByComment is null) throw new NotFoundException("Comment is null");
        _carCommentWriteRepository.Remove(ByComment);
        await _carCommentWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid id, CarCommentUpdateDTO carCommentUpdateDTO)
    {
        var ByComment = await _carCommentReadRepository.GetByIdAsync(id);
        if (ByComment is null) throw new NotFoundException("Comment is null");

        _mapper.Map(carCommentUpdateDTO, ByComment);
        _carCommentWriteRepository.Update(ByComment);
        await _carCommentWriteRepository.SavaChangeAsync();
    }
}
