using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.DTOs.CarType;
using EndProjet.Persistance.Exceptions;
using EndProjet.Persistance.Implementations.Services;
using Moq;
using System.Linq.Expressions;

namespace LuxeDriveUnitTest.Services.CarType;

public class CarTypeServiceTests
{
    private readonly CarTypeService _carTypeService;
    private readonly Mock<ICarTypeReadRepository> _mockTypeReadRepository;
    private readonly Mock<ICarTypeWriteRepository> _mockTypeWriteRepository;
    private readonly Mock<IMapper> _mockMapper;

    public CarTypeServiceTests()
    {
        _mockTypeReadRepository = new Mock<ICarTypeReadRepository>();
        _mockTypeWriteRepository = new Mock<ICarTypeWriteRepository>();
        _mockMapper = new Mock<IMapper>();

        _carTypeService = new CarTypeService(_mockTypeReadRepository.Object, _mockTypeWriteRepository.Object,
              _mockMapper.Object);
    }

    [Fact]
    public async Task CreateAsync_WithValidCarTypeCreateDTO_CreatesNewCarTypeAndSavesChanges()
    {
        // Arrange
        var carTypeCreateDTO = new CarTypeCreateDTO { type = "Sedan" };
        var fakeCarType = new EndProject.Domain.Entitys.CarType();

        _mockMapper.Setup(mapper => mapper.Map<EndProject.Domain.Entitys.CarType>(carTypeCreateDTO))
                   .Returns(fakeCarType);

        // Act
        await _carTypeService.CreateAsync(carTypeCreateDTO);

        // Assert
        _mockTypeWriteRepository.Verify(repo => repo.AddAsync(fakeCarType), Times.Once);
        _mockTypeWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }


    //[Fact]
    //public async Task GetAllAsync_WithValidData_ReturnsListOfCarTypeGetDTO()
    //{
    //    // Arrange
    //    var fakeCarTypes = new List<EndProject.Domain.Entitys.CarType> 
    //    { new EndProject.Domain.Entitys.CarType(), 
    //        new EndProject.Domain.Entitys.CarType(), 
    //           new EndProject.Domain.Entitys.CarType() };
    //    var fakeCarTypeGetDTOs = fakeCarTypes.Select(ct => new CarTypeGetDTO()).ToList();

    //    _mockTypeReadRepository.Setup(repo => repo.GetAll()).Returns(fakeCarTypes.AsQueryable());
    //    _mockMapper.Setup(mapper => mapper.Map<List<CarTypeGetDTO>>(fakeCarTypes)).Returns(fakeCarTypeGetDTOs);

    //    // Act
    //    var result = await _carTypeService.GetAllAsync();

    //    // Assert
    //    Assert.Equal(fakeCarTypeGetDTOs, result);
    //}

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsCarTypeGetDTO()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeCarType = new EndProject.Domain.Entitys.CarType { Id = validId };
        var fakeCarTypeGetDTO = new CarTypeGetDTO();

        _mockTypeReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeCarType);
        _mockMapper.Setup(mapper => mapper.Map<CarTypeGetDTO>(fakeCarType)).Returns(fakeCarTypeGetDTO);

        // Act
        var result = await _carTypeService.GetByIdAsync(validId);

        // Assert
        Assert.Equal(fakeCarTypeGetDTO, result);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        _mockTypeReadRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((EndProject.Domain.Entitys.CarType)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _carTypeService.GetByIdAsync(invalidId));
    }


    [Fact]
    public async Task GetByNameAsync_WithValidType_ReturnsCarTypeGetDTO()
    {
        // Arrange
        string validType = "SUV";
        var fakeCarType = new EndProject.Domain.Entitys.CarType { type = validType };
        var fakeCarTypeGetDTO = new CarTypeGetDTO();

        _mockTypeReadRepository.Setup(repo => repo.GetByIdAsyncExpression(
            It.IsAny<Expression<Func<EndProject.Domain.Entitys.CarType, bool>>>(), It.IsAny<bool>())).ReturnsAsync(fakeCarType);

        _mockMapper.Setup(mapper => mapper.Map<CarTypeGetDTO>(fakeCarType)).Returns(fakeCarTypeGetDTO);

        // Act
        var result = await _carTypeService.GetByNameAsync(validType);

        // Assert
        Assert.Equal(fakeCarTypeGetDTO, result);
    }

    [Fact]
    public async Task GetByNameAsync_WithInvalidType_ThrowsNotFoundException()
    {
        // Arrange
        string invalidType = "InvalidType";

        _mockTypeReadRepository.Setup(repo => repo.GetByIdAsyncExpression(
            It.IsAny<Expression<Func<EndProject.Domain.Entitys.CarType, bool>>>(), It.IsAny<bool>())).ReturnsAsync((EndProject.Domain.Entitys.CarType)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _carTypeService.GetByNameAsync(invalidType));
    }

    [Fact]
    public async Task RemoveAsync_WithValidId_RemovesCarType()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeCarType = new EndProject.Domain.Entitys.CarType { Id = validId };

        _mockTypeReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeCarType);

        // Act
        await _carTypeService.RemoveAsync(validId);

        // Assert
        _mockTypeWriteRepository.Verify(repo => repo.Remove(fakeCarType), Times.Once);
        _mockTypeWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_WithInvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        _mockTypeReadRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((EndProject.Domain.Entitys.CarType)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _carTypeService.RemoveAsync(invalidId));
    }

    [Fact]
    public async Task UpdateAsync_WithValidId_UpdatesCarType()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var carTypeUpdateDTO = new CarTypeUpdateDTO { /* Set properties for update */ };
        var fakeCarType = new EndProject.Domain.Entitys.CarType { Id = validId };

        _mockTypeReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeCarType);

        // Act
        await _carTypeService.UpdateAsync(validId, carTypeUpdateDTO);

        // Assert
        _mockMapper.Verify(mapper => mapper.Map(carTypeUpdateDTO, fakeCarType), Times.Once);
        _mockTypeWriteRepository.Verify(repo => repo.Update(fakeCarType), Times.Once);
        _mockTypeWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();
        var carTypeUpdateDTO = new CarTypeUpdateDTO { /* Set properties for update */ };

        _mockTypeReadRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((EndProject.Domain.Entitys.CarType)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _carTypeService.UpdateAsync(invalidId, carTypeUpdateDTO));
    }
}
