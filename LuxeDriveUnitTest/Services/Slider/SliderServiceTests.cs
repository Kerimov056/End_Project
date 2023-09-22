using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProjet.Persistance.Implementations.Services;
using Moq;

namespace LuxeDriveUnitTest.Services.Slider;

public class SliderServiceTests
{
    private readonly SliderService _sliderService;
    private readonly Mock<ISliderReadRepository> _sliderReadRepositoryMock = new();
    private readonly Mock<ISliderWriteRepository> _sliderWriteRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IQRCoderServıces> _qrcoderServMock = new();
    //private readonly Mock<IStringLocalizer> _stringLocalizerMock = new();

    public SliderServiceTests()
    {
        _sliderService = new SliderService(_sliderReadRepositoryMock.Object,
                                           _sliderWriteRepositoryMock.Object,
                                           _mapperMock.Object,
                                           _qrcoderServMock.Object);
    }

    //[Fact]

    //public async Task GetByIdAsync_ShouldReturnSliderDto_WhenExsistId()
    //{
    //    //Arrange
    //    //var Id = 1;
    //    //var imagePath = "Salam.png";
    //    //Slider slider = new()
    //    //{
            
    //    //};
    //}
}
