using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.DTOs.Category;
using EndProjet.Persistance.Exceptions;
using EndProjet.Persistance.Implementations.Services;
using Moq;

namespace LuxeDriveUnitTest.Services.CarCategory;

public class CarCategoryServicesTests
{
    private readonly CarCategoryServices _carCategoryServices;
    private readonly Mock<ICarCategoryReadRepository> _mockCategoryReadRepository;
    private readonly Mock<ICarCategoryWriteRepository> _mockCategoryWriteRepository;
    private readonly Mock<IMapper> _mockMapper;

    public CarCategoryServicesTests()
    {
        _mockCategoryReadRepository = new Mock<ICarCategoryReadRepository>();
        _mockCategoryWriteRepository = new Mock<ICarCategoryWriteRepository>();
        _mockMapper = new Mock<IMapper>();

        _carCategoryServices = new CarCategoryServices(_mockCategoryReadRepository.Object, _mockCategoryWriteRepository.Object,
              _mockMapper.Object);
    }


    [Fact]
    public async Task CreateAsync_ValidInput_CreatesNewCarCategoryAndSavesChanges()
    {
        // Arrange
        var carCategoryCreateDTO = new CarCategoryCreateDTO(); 

        _mockMapper.Setup(mapper => mapper.Map<EndProject.Domain.Entitys.CarCategory>(carCategoryCreateDTO))
                   .Returns(new EndProject.Domain.Entitys.CarCategory());

        // Act
        await _carCategoryServices.CreateAsync(carCategoryCreateDTO);

        // Assert
        _mockCategoryWriteRepository.Verify(repo => repo.AddAsync(It.IsAny<EndProject.Domain.Entitys.CarCategory>()), Times.Once);
        _mockCategoryWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }


    //[Fact]
    //public async Task GetAllAsync_ValidData_ReturnsListOfCarCategoryGetDTO()
    //{
    //    // Arrange
    //    var fakeCategories = new List<EndProject.Domain.Entitys.CarCategory> 
    //    { new EndProject.Domain.Entitys.CarCategory(), 
    //        new EndProject.Domain.Entitys.CarCategory(), 
    //          new EndProject.Domain.Entitys.CarCategory() };
    //    var fakeCarCategoryGetDTOs = fakeCategories.Select(c => new CarCategoryGetDTO()).ToList();

    //    _mockCategoryReadRepository.Setup(repo => repo.GetAll().ToListAsync())
    //                               .ReturnsAsync(fakeCategories);

    //    _mockMapper.Setup(mapper => mapper.Map<List<CarCategoryGetDTO>>(fakeCategories))
    //               .Returns(fakeCarCategoryGetDTOs);

    //    // Act
    //    var result = await _carCategoryServices.GetAllAsync();

    //    // Assert
    //    Assert.Equal(fakeCarCategoryGetDTOs, result);
    //}


    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsCarCategoryGetDTO()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeCarCategory = new EndProject.Domain.Entitys.CarCategory { Id = validId, Category = "SUV" };
        var fakeCarCategoryGetDTO = new CarCategoryGetDTO { Id = validId, Category = "SUV" };

        _mockCategoryReadRepository.Setup(repo => repo.GetByIdAsync(validId))
                                   .ReturnsAsync(fakeCarCategory);

        _mockMapper.Setup(mapper => mapper.Map<CarCategoryGetDTO>(fakeCarCategory))
                   .Returns(fakeCarCategoryGetDTO);

        // Act
        var result = await _carCategoryServices.GetByIdAsync(validId);

        // Assert
        Assert.Equal(fakeCarCategoryGetDTO, result);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        _mockCategoryReadRepository.Setup(repo => repo.GetByIdAsync(invalidId))
                                   .ReturnsAsync((EndProject.Domain.Entitys.CarCategory)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _carCategoryServices.GetByIdAsync(invalidId));
    }


    [Fact]
    public async Task RemoveAsync_WithValidId_RemovesCarCategoryAndSavesChanges()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeCarCategory = new EndProject.Domain.Entitys.CarCategory { Id = validId, Category = "SUV" };

        _mockCategoryReadRepository.Setup(repo => repo.GetByIdAsync(validId))
                                   .ReturnsAsync(fakeCarCategory);

        // Act
        await _carCategoryServices.RemoveAsync(validId);

        // Assert
        _mockCategoryWriteRepository.Verify(repo => repo.Remove(fakeCarCategory), Times.Once);
        _mockCategoryWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_WithInvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        _mockCategoryReadRepository.Setup(repo => repo.GetByIdAsync(invalidId))
                                   .ReturnsAsync((EndProject.Domain.Entitys.CarCategory)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _carCategoryServices.RemoveAsync(invalidId));
    }


    [Fact]
    public async Task UpdateAsync_WithValidId_UpdatesCarCategoryAndSavesChanges()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var carCategoryUpdateDTO = new CarCategoryUpdateDTO { category = "SUV" };
        var fakeCarCategory = new EndProject.Domain.Entitys.CarCategory { Id = validId, Category = "Compact" };

        _mockCategoryReadRepository.Setup(repo => repo.GetByIdAsync(validId))
                                   .ReturnsAsync(fakeCarCategory);

        // Act
        await _carCategoryServices.UpdateAsync(validId, carCategoryUpdateDTO);

        // Assert
        _mockMapper.Verify(mapper => mapper.Map(carCategoryUpdateDTO, fakeCarCategory), Times.Once);
        _mockCategoryWriteRepository.Verify(repo => repo.Update(fakeCarCategory), Times.Once);
        _mockCategoryWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();
        var carCategoryUpdateDTO = new CarCategoryUpdateDTO { category = "SUV" };

        _mockCategoryReadRepository.Setup(repo => repo.GetByIdAsync(invalidId))
                                   .ReturnsAsync((EndProject.Domain.Entitys.CarCategory)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _carCategoryServices.UpdateAsync(invalidId, carCategoryUpdateDTO));
    }
}
