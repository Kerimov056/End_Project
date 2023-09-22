using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Domain.Entitys.Identity;
using EndProjet.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace LuxeDriveUnitTest.Services.Car;

public class CarServicesTests
{
    private readonly CarServices _carServices;
    private readonly Mock<ICarReadRepository> _mockCarReadRepository;
    private readonly Mock<ICarWriteRepository> _mockCarWriteRepository;
    private readonly Mock<ICarTypeService> _mockCarTypeService;
    private readonly Mock<ICarTypeWriteRepository> _mockCarTypeWriteRepository;
    private readonly Mock<ICarImageServices> _mockCarImageServices;
    private readonly Mock<ITagReadRepository> _mockTagReadRepository;
    private readonly Mock<ITagWriteRepository> _mockTagWriteRepository;
    private readonly Mock<ICarCategoryWriteRepository> _mockCarCategoryWriteRepository;
    private readonly Mock<ICarTagWriteRepository> _mockCarTagWriteRepository;
    private readonly Mock<ICarTagReadRepository> _mockCarTagReadRepository;
    private readonly Mock<ICarReservationReadRepository> _mockCarReservationReadRepository;
    private readonly Mock<UserManager<AppUser>> _mockUserManager;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly Mock<IQRCoderServıces> _mockIQRCoderServ;
    private readonly Mock<IMapper> _mockMapper;

    public CarServicesTests()
    {
        _mockCarReadRepository = new Mock<ICarReadRepository>();
        _mockCarWriteRepository = new Mock<ICarWriteRepository>();
        _mockCarTypeService = new Mock<ICarTypeService>();
        _mockCarTypeWriteRepository = new Mock<ICarTypeWriteRepository>();
        _mockCarImageServices = new Mock<ICarImageServices>();
        _mockTagReadRepository = new Mock<ITagReadRepository>();
        _mockTagWriteRepository = new Mock<ITagWriteRepository>();
        _mockCarCategoryWriteRepository = new Mock<ICarCategoryWriteRepository>();
        _mockCarTagWriteRepository = new Mock<ICarTagWriteRepository>();
        _mockCarTagReadRepository = new Mock<ICarTagReadRepository>();
        _mockCarReservationReadRepository = new Mock<ICarReservationReadRepository>();
        _mockUserManager = new Mock<UserManager<AppUser>>();
        _mockEmailService = new Mock<IEmailService>();
        _mockIQRCoderServ = new Mock<IQRCoderServıces>();
        _mockMapper = new Mock<IMapper>();


        _carServices = new CarServices(_mockCarReadRepository.Object, _mockCarWriteRepository.Object, _mockMapper.Object,
            _mockCarTypeService.Object, _mockCarTypeWriteRepository.Object, _mockCarImageServices.Object,
            _mockTagReadRepository.Object, _mockTagWriteRepository.Object, _mockCarCategoryWriteRepository.Object,
            _mockCarTagWriteRepository.Object, _mockCarTagReadRepository.Object, _mockCarReservationReadRepository.Object,
            _mockUserManager.Object, _mockEmailService.Object, _mockIQRCoderServ.Object);
    }


    //[Fact]
    //public async Task GetCarCountAsync_WhenCalled_ReturnsCarCount()
    //{
    //    // Arrange
    //    int expectedCarCount = 10;
    //    _mockCarReadRepository.Setup(repo => repo.GetCarCountAsync())
    //                          .ReturnsAsync(expectedCarCount);

    //    // Act
    //    int actualCarCount = await _carServices.GetCarCountAsync();

    //    // Assert
    //    Assert.Equal(expectedCarCount, actualCarCount);
    //}

    //[Fact]
    //public async Task GetReservCarCountAsync_WhenCalled_ReturnsReservCarCount()
    //{
    //    // Arrange
    //    int expectedReservCarCount = 5;
    //    _mockCarReadRepository.Setup(repo => repo.GetReservCarCountAsync())
    //                          .ReturnsAsync(expectedReservCarCount);

    //    // Act
    //    int actualReservCarCount = await _carServices.GetReservCarCountAsync();

    //    // Assert
    //    Assert.Equal(expectedReservCarCount, actualReservCarCount);
    //}

    //[Fact]
    //public async Task RemoveAsync_WithValidId_RemovesCarAndSavesChanges()
    //{
    //    // Arrange
    //    Guid validId = Guid.NewGuid();

    //    // Mock CarReadRepository
    //    var fakeCar = new EndProject.Domain.Entitys.Car { Id = validId, ;
    //    _mockCarReadRepository.Setup(repo => repo.GetAll())
    //                          .Returns(new List<EndProject.Domain.Entitys.Car> { fakeCar }.AsQueryable());

    //    // Mock CarWriteRepository
    //    _mockCarWriteRepository.Setup(repo => repo.Remove(It.IsAny<EndProject.Domain.Entitys.Car>()));

    //    // Act
    //    await _carServices.RemoveAsync(validId);

    //    // Assert
    //    _mockCarWriteRepository.Verify(repo => repo.Remove(fakeCar), Times.Once);
    //    _mockCarWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    //}


}
