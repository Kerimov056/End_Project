using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.Abstraction.Services.Stroge;
using EndProject.Application.DTOs.Slider;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Common;
using EndProjet.Persistance.ExtensionsMethods;
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

        if (sliderCreateDTO.image is not null)
        {
            DtoToEntity.Imagepath = await sliderCreateDTO.image.GetBytes();
        }
        await _sliderWriteRepository.AddAsync(DtoToEntity);
        await _sliderWriteRepository.SavaChangeAsync();
    }

    public async Task<List<SliderGetDTO>> GetAllAsync()
    {
        var silder = await _sliderReadRepository.GetAll().OrderByDescending(x=>x.CreatedDate).ToListAsync();
        if (silder is null) throw new NullReferenceException();
        var EntityToDto = _mapper.Map<List<SliderGetDTO>>(silder);
        foreach (var item in EntityToDto)
        {
            Slider sliderTo = silder.FirstOrDefault(x => x.Id == item.Id)   
                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

            List<string> images = new();
            images.Add(Convert.ToBase64String(sliderTo.Imagepath));
            item.imagePath = images[0];
        }
        return EntityToDto;
    }

    public async Task<SliderGetDTO> GetByIdAsync(Guid Id)
    {
        var BySlider = await _sliderReadRepository.GetByIdAsync(Id);
        if (BySlider is null) throw new NullReferenceException();
        var EntityToDto = _mapper.Map<SliderGetDTO>(BySlider);
        EntityToDto.imagePath = Convert.ToBase64String(BySlider.Imagepath);
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
        if (sliderUptadeDTO.image is not null) BySlider.Imagepath = await sliderUptadeDTO.image.GetBytes();
        _sliderWriteRepository.Update(BySlider);
        await _sliderWriteRepository.SavaChangeAsync();
    }

}