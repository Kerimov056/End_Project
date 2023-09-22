using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Slider;
using EndProjet.Persistance.Implementations.Services;
using Moq;

namespace LuxeDriveUnitTest.Services.Slider;

public class SliderServiceTests
{
    private readonly SliderService _sliderService;
    private readonly Mock<ISliderReadRepository> _mockSliderReadRepositoryMock;
    private readonly Mock<ISliderWriteRepository> _mockSliderWriteRepositoryMock;
    private readonly Mock<IMapper> _mockMapperMock;
    private readonly Mock<IQRCoderServıces> _mockQrcoderServMock;

    public SliderServiceTests()
    {
        _mockSliderReadRepositoryMock = new Mock<ISliderReadRepository>();
        _mockSliderWriteRepositoryMock = new Mock<ISliderWriteRepository>();
        _mockMapperMock = new Mock<IMapper>();
        _mockQrcoderServMock = new Mock<IQRCoderServıces>();

        _sliderService = new SliderService(_mockSliderReadRepositoryMock.Object,
                                           _mockSliderWriteRepositoryMock.Object,
                                           _mockMapperMock.Object,
                                           _mockQrcoderServMock.Object);
    }

    ////Create
    //[Fact]
    //public async Task CreateAsync_ValidSliderCreateDTO_CreatesSlider()
    //{
    //    // Arrange
    //    var fakeSliderCreateDTO = new SliderCreateDTO
    //    {

    //    };

    //    var fakeSliderEntity = new EndProject.Domain.Entitys.Slider(); // Create a fake Slider entity

    //    _mockMapperMock.Setup(mapper => mapper.Map<EndProject.Domain.Entitys.Slider>(fakeSliderCreateDTO)).Returns(fakeSliderEntity);

    //    // Optionally, set up a fake image for testing (if needed)
    //    fakeSliderCreateDTO.image = new YourImageClass(); // Replace YourImageClass with your actual image class

    //    _mockSliderWriteRepositoryMock.Setup(repo => repo.AddAsync(fakeSliderEntity)).Returns(Task.CompletedTask);

    //    // Act
    //    await _sliderService.CreateAsync(fakeSliderCreateDTO);

    //    // Assert
    //    _mockMapperMock.Verify(mapper => mapper.Map<EndProject.Domain.Entitys.Slider>(fakeSliderCreateDTO), Times.Once);
    //    _mockSliderWriteRepositoryMock.Verify(repo => repo.AddAsync(fakeSliderEntity), Times.Once);
    //    _mockSliderWriteRepositoryMock.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    //}



    ////GetAll
    //[Fact]
    //public async Task GetAllAsync_WithValidData_ReturnsListOfSliderGetDTO()
    //{
    //    // Arrange
    //    var fakeSliders = new List<EndProject.Domain.Entitys.Slider>
    //{
    //    new EndProject.Domain.Entitys.Slider { Id = Guid.NewGuid(), CreatedDate = DateTime.Now, Imagepath = new byte[] { 1, 2, 3 } },
    //    new EndProject.Domain.Entitys.Slider { Id = Guid.NewGuid(), CreatedDate = DateTime.Now, Imagepath = new byte[] { 4, 5, 6 } },
    //    new EndProject.Domain.Entitys.Slider { Id = Guid.NewGuid(), CreatedDate = DateTime.Now, Imagepath = new byte[] { 7, 8, 9 } }
    //};

    //    var fakeSliderGetDTOs = fakeSliders.Select(s => new SliderGetDTO { Id = s.Id }).ToList();

    //    _mockSliderReadRepositoryMock.Setup(repo => repo.GetAll()).Returns(fakeSliders.AsQueryable());
    //    _mockMapperMock.Setup(mapper => mapper.Map<List<SliderGetDTO>>(fakeSliders)).Returns(fakeSliderGetDTOs);

    //    // Act
    //    var result = await _sliderService.GetAllAsync();

    //    // Assert
    //    Assert.Equal(fakeSliderGetDTOs.Count, result.Count);

    //    for (int i = 0; i < fakeSliderGetDTOs.Count; i++)
    //    {
    //        Assert.Equal(fakeSliderGetDTOs[i].Id, result[i].Id);
    //        Assert.Equal(Convert.ToBase64String(fakeSliders[i].Imagepath), result[i].imagePath);
    //    }
    //}

    //[Fact]
    //public async Task GetAllAsync_WithNoData_ThrowsNullReferenceException()
    //{
    //    // Arrange
    //    _mockSliderReadRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<EndProject.Domain.Entitys.Slider>().AsQueryable());

    //    // Act and Assert
    //    await Assert.ThrowsAsync<NullReferenceException>(() => _sliderService.GetAllAsync());
    //}




    //GetById
    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsSliderGetDTO()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeSlider = new EndProject.Domain.Entitys.Slider
        {
            Id = validId,
            Imagepath = new byte[] { 1, 2, 3 } // Replace with actual image bytes
        };

        var fakeSliderGetDTO = new SliderGetDTO();

        _mockSliderReadRepositoryMock.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeSlider);
        _mockMapperMock.Setup(mapper => mapper.Map<SliderGetDTO>(fakeSlider)).Returns(fakeSliderGetDTO);

        // Act
        var result = await _sliderService.GetByIdAsync(validId);

        // Assert
        Assert.Equal(fakeSliderGetDTO, result);
        Assert.Equal(Convert.ToBase64String(fakeSlider.Imagepath), result.imagePath);
    }

    [Fact]
    public async Task GetByIdAsync_InvalidId_ThrowsNullReferenceException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        _mockSliderReadRepositoryMock.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((EndProject.Domain.Entitys.Slider)null);

        // Act and Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => _sliderService.GetByIdAsync(invalidId));
    }


    //Remove
    [Fact]
    public async Task RemoveAsync_ValidId_RemovesSlider()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeSlider = new EndProject.Domain.Entitys.Slider { Id = validId }; // Create a fake Slider entity

        _mockSliderReadRepositoryMock.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeSlider);

        // Act
        await _sliderService.RemoveAsync(validId);

        // Assert
        _mockSliderWriteRepositoryMock.Verify(repo => repo.Remove(fakeSlider), Times.Once);
        _mockSliderWriteRepositoryMock.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_InvalidId_ThrowsNullReferenceException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        _mockSliderReadRepositoryMock.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((EndProject.Domain.Entitys.Slider)null);

        // Act and Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => _sliderService.RemoveAsync(invalidId));
    }



    ////Update
    //[Fact]
    //public async Task UpdateAsync_ValidIdAndDTO_UpdatesSlider()
    //{
    //    // Arrange
    //    Guid validId = Guid.NewGuid();
    //    var fakeSlider = new EndProject.Domain.Entitys.Slider { Id = validId }; // Create a fake Slider entity
    //    var fakeSliderUpdateDTO = new SliderUpdateDTO
    //    {
    //        // Set properties of the DTO as needed for the test
    //        image = new YourImageClass() // Replace YourImageClass with your actual image class
    //    };

    //    _mockSliderReadRepositoryMock.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeSlider);

    //    // Act
    //    await _sliderService.UpdateAsync(validId, fakeSliderUpdateDTO);

    //    // Assert
    //    _mockMapperMock.Verify(mapper => mapper.Map(fakeSliderUpdateDTO, fakeSlider), Times.Once);
    //    if (fakeSliderUpdateDTO.image is not null)
    //    {
    //        _mockSliderWriteRepositoryMock.Verify(repo => repo.Update(fakeSlider), Times.Once);
    //        _mockSliderWriteRepositoryMock.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    //    }
    //}

    //[Fact]
    //public async Task UpdateAsync_InvalidId_ThrowsNullReferenceException()
    //{
    //    // Arrange
    //    Guid invalidId = Guid.NewGuid();
    //    var fakeSliderUpdateDTO = new SliderUpdateDTO();

    //    _mockSliderReadRepositoryMock.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((EndProject.Domain.Entitys.Slider)null);

    //    // Act and Assert
    //    await Assert.ThrowsAsync<NullReferenceException>(() => _sliderService.UpdateAsync(invalidId, fakeSliderUpdateDTO));
    //}

}
