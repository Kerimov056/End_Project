using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Slider;
using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class SliderService : ISliderService
{
    private readonly ISliderReadRepository _sliderReadRepository;
    private readonly ISliderWriteRepository _sliderWriteRepository;
    private readonly IStorageFile _uploadFile;
    private readonly IMapper _mapper;
    public SliderService(ISliderReadRepository sliderReadRepository,
                         ISliderWriteRepository sliderWriteRepository,
                         IMapper mapper,
                         IStorageFile uploadFile)
    {
        _sliderReadRepository = sliderReadRepository;
        _sliderWriteRepository = sliderWriteRepository;
        _uploadFile = uploadFile;
        _mapper = mapper;
    }

    public async Task CreateAsync(SliderCreateDTO sliderCreateDTO)
    {
        var DtoToEntity = _mapper.Map<Slider>(sliderCreateDTO);
        if (sliderCreateDTO.image != null && sliderCreateDTO.image.Length > 0)
        {
            var ImagePath = await _uploadFile.WriteFile("Upload\\Files", sliderCreateDTO.image);
            DtoToEntity.Imagepath = ImagePath;
        }
        await _sliderWriteRepository.AddAsync(DtoToEntity);
        await _sliderWriteRepository.SavaChangeAsync();
    }

    public async Task<List<SliderGetDTO>> GetAllAsync()
    {
        var silder = await _sliderReadRepository.GetAll().ToListAsync();
        if (silder is null) throw new NullReferenceException();
        var EntityToDto = _mapper.Map<List<SliderGetDTO>>(silder);
        return EntityToDto;
    }

    public async Task<SliderGetDTO> GetByIdAsync(Guid Id)
    {
        var BySlider = await _sliderReadRepository.GetByIdAsync(Id);
        if (BySlider is null) throw new NullReferenceException();
        var EntityToDto = _mapper.Map<SliderGetDTO>(BySlider);
        return EntityToDto;
    }

    public async Task RemoveAsync(Guid id)
    {
        var BySlider = await _sliderReadRepository.GetByIdAsync(id);
        if (BySlider is null) throw new NullReferenceException();
        _sliderWriteRepository.Remove(BySlider);
        await _sliderWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid id, SliderUpdateDTO sliderUptadeDTO)
    {
        var BySlider = await _sliderReadRepository.GetByIdAsync(id);
        if (BySlider is null) throw new NullReferenceException();
        _mapper.Map(sliderUptadeDTO, BySlider);
        if (sliderUptadeDTO.image != null && sliderUptadeDTO.image.Length > 0)
        {
            var ImagePath = await _uploadFile.WriteFile("Upload\\Files", sliderUptadeDTO.image);
            BySlider.Imagepath = ImagePath;
        }
        _sliderWriteRepository.Update(BySlider);
        await _sliderWriteRepository.SavaChangeAsync();
    }

}