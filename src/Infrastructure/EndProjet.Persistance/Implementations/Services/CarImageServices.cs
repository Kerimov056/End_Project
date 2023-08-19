using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarImage;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class CarImageServices : ICarImageServices
{
    private readonly ICarImageReadRepository _carImageReadRepository;
    private readonly ICarImageWriteRepository _carImageWriteRepository;
    private readonly IStorageFile _storageFile;
    private readonly IMapper _mapper;

    public CarImageServices(ICarImageReadRepository carImageReadRepository,
                            ICarImageWriteRepository carImageWriteRepository,
                            IStorageFile storageFile,
                            IMapper mapper)
    {
        _carImageReadRepository = carImageReadRepository;
        _carImageWriteRepository = carImageWriteRepository;
        _storageFile = storageFile;
        _mapper = mapper;
    }

    public async Task CreateAsync(CarImageCreateDTO carImageCreateDTO)
    { 
        var ToEntity = _mapper.Map<CarImage>(carImageCreateDTO);
        if (carImageCreateDTO.image != null && carImageCreateDTO.image.Length > 0)
        {
            var ImagePath = await _storageFile.WriteFile("Upload\\Files", carImageCreateDTO.image);
            ToEntity.imagePath = ImagePath;
        }
        await _carImageWriteRepository.AddAsync(ToEntity);
        await _carImageWriteRepository.SavaChangeAsync();
    }

    public async Task<List<CarImageGetDTO>> GetAllAsync()
    {
        var CarImageAll = await _carImageReadRepository.GetAll().ToListAsync();
        if (CarImageAll is null) throw new NotFoundException("CarImage is Null");

        var ToDto = _mapper.Map<List<CarImageGetDTO>>(CarImageAll);
        return ToDto;
    }

    public async Task<CarImageGetDTO> GetByIdAsync(Guid Id)
    {
        var ByCarImage = await _carImageReadRepository.GetByIdAsync(Id);
        if (ByCarImage is null) throw new NotFoundException("CarImage is Null");

        var ToDto = _mapper.Map<CarImageGetDTO>(ByCarImage);
        return ToDto;
    }

    public async Task RemoveAsync(Guid id)
    {
        var ByCarImage = await _carImageReadRepository.GetByIdAsync(id);
        if (ByCarImage is null) throw new NotFoundException("CarImage is Null");

        _carImageWriteRepository.Remove(ByCarImage);
        await _carImageWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid id, CarImageUpdateDTO carImageUpdateDTO)
    {
        var ByCarImage = await _carImageReadRepository.GetByIdAsync(id);
        if (ByCarImage is null) throw new NotFoundException("CarImage is Null");

        _mapper.Map(carImageUpdateDTO, ByCarImage);
        _carImageWriteRepository.Update(ByCarImage);
        await _carImageWriteRepository.SavaChangeAsync();
    }
}
