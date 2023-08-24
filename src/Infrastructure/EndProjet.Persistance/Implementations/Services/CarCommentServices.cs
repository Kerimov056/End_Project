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
    private readonly ICarServices _carServices;
    private readonly IMapper _mapper;

    public CarCommentServices(ICarCommentReadRepository carCommentReadRepository,
                              ICarCommentWriteRepository carCommentWriteRepository,
                              ICarServices carServices,
                              IMapper mapper)
    {
        _carCommentReadRepository = carCommentReadRepository;
        _carCommentWriteRepository = carCommentWriteRepository;
        _carServices = carServices;
        _mapper = mapper;
    }

    public async Task CreateAsync(CarCommentCreateDTO carCommentCreateDTO)
    {
        var newCarCommnet = _mapper.Map<CarComment>(carCommentCreateDTO);
        newCarCommnet.Like = null;
        await _carCommentWriteRepository.AddAsync(newCarCommnet);
        await _carCommentWriteRepository.SavaChangeAsync();
    }

    public async Task<List<CarCommentGetDTO>> GetAllAsync(Guid CarId)
    {
        var ByCar = await _carServices.GetByIdAsync(CarId);

        var CarAllComment = await _carCommentReadRepository
                            .GetAll()
                            .Include(x => x.Like)
                            .Include(x => x.Car)
                            .Where(x => x.Car.Id == CarId)
                            .ToListAsync();
        if (CarAllComment is null) throw new NotFoundException("Comment is null");

        var ToDto = _mapper.Map<List<CarCommentGetDTO>>(CarAllComment);
        foreach (var item in ToDto)
        {
            foreach (var car in CarAllComment)
            {
                if (item.Id == car.Id)
                {
                    if (car.Like is not null)
                        item.LikeSum = car.Like.Count;
                    else
                        item.LikeSum = 0;
                    break;
                }
            }
        }
        return ToDto;
    }

    public async Task<CarCommentGetDTO> GetByIdAsync(Guid Id)
    {
        var ByComment = await _carCommentReadRepository
            .GetAll()
            .Include(x =>x.Like)
            .Where(x => x.Id == Id)
            .FirstOrDefaultAsync();
        if (ByComment is null) throw new NotFoundException("Comment is Null");

        var ToDto = _mapper.Map<CarCommentGetDTO>(ByComment);
        if (ByComment.Like is not null)
          ToDto.LikeSum = ByComment.Like.Count;
        else
            ToDto.LikeSum = 0;
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
        ByComment.Like = ByComment.Like;
        _carCommentWriteRepository.Update(ByComment);
        await _carCommentWriteRepository.SavaChangeAsync();
    }
}
