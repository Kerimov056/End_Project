using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Advantage;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;
using EndProjet.Persistance.Implementations.Services;
using Moq;

namespace LuxeDriveUnitTest.Services.Advantages;

public class AdvantageServicesTests
{
    private AdvantageServices _advantageServices;
    private Mock<IAdvantageReadRepository> _mockAdvantagesReadRepository;
    private Mock<IAdvantageWriteRepository> _mockAdvantagesWriteRepository;
    private Mock<IMapper> _mockMapper;
    private Mock<IQRCoderServıces> _mockQRCoderServices;

    public AdvantageServicesTests()
    {
        _mockAdvantagesReadRepository = new Mock<IAdvantageReadRepository>();
        _mockAdvantagesWriteRepository = new Mock<IAdvantageWriteRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockQRCoderServices = new Mock<IQRCoderServıces>();

        _advantageServices = new AdvantageServices(
            _mockAdvantagesReadRepository.Object,
            _mockAdvantagesWriteRepository.Object,
            _mockMapper.Object,
            _mockQRCoderServices.Object
        );
    }

    ////GetAll
    //[Fact]
    //public async Task GetAllAsync_WithValidData_ReturnsListOfAdvantageGetDTO()
    //{
    //    // Arrange
    //    var fakeAdvantages = new List<Advantage> { new Advantage(), new Advantage(), new Advantage() };
    //    var fakeAdvantageGetDTOs = fakeAdvantages.Select(a => new AdvantageGetDTO()).ToList();

    //    _mockAdvantagesReadRepository.Setup(repo => repo.GetAll()).Returns(fakeAdvantages.AsQueryable());
    //    _mockMapper.Setup(mapper => mapper.Map<List<AdvantageGetDTO>>(fakeAdvantages)).Returns(fakeAdvantageGetDTOs);

    //    // Act
    //    var result = await _advantageServices.GetAllAsync();

    //    // Assert
    //    Assert.Equal(fakeAdvantageGetDTOs, result);
    //}

    //[Fact]
    //public async Task GetAllAsync_WithNoData_ThrowsNotFoundException()
    //{
    //    // Arrange
    //    _mockAdvantagesReadRepository.Setup(repo => repo.GetAll()).Returns(new List<Advantage>().AsQueryable());

    //    // Act and Assert
    //    await Assert.ThrowsAsync<NotFoundException>(() => _advantageServices.GetAllAsync());
    //}






    //GetById
    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsAdvantageGetDTO()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeAdvantage = new Advantage(); // Create a fake Advantage object with the validId
        var fakeAdvantageGetDTO = new AdvantageGetDTO(); // Create a corresponding DTO

        _mockAdvantagesReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeAdvantage);
        _mockMapper.Setup(mapper => mapper.Map<AdvantageGetDTO>(fakeAdvantage)).Returns(fakeAdvantageGetDTO);

        // Act
        var result = await _advantageServices.GetByIdAsync(validId);

        // Assert
        Assert.Equal(fakeAdvantageGetDTO, result);
    }

    [Fact]
    public async Task GetByIdAsync_InvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        _mockAdvantagesReadRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((Advantage)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _advantageServices.GetByIdAsync(invalidId));
    }


    //Remove
    [Fact]
    public async Task RemoveAsync_ValidId_RemovesAdvantage()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeAdvantage = new Advantage { Id = validId };

        _mockAdvantagesReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeAdvantage);

        // Act
        await _advantageServices.RemoveAsync(validId);

        // Assert
        _mockAdvantagesWriteRepository.Verify(repo => repo.Remove(fakeAdvantage), Times.Once);
        _mockAdvantagesWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_InvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        _mockAdvantagesReadRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((Advantage)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _advantageServices.RemoveAsync(invalidId));

        // Verify that Remove and SaveChangeAsync were not called
        _mockAdvantagesWriteRepository.Verify(repo => repo.Remove(It.IsAny<Advantage>()), Times.Never);
        _mockAdvantagesWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Never);
    }


    //Update
    [Fact]
    public async Task UpdateAsync_ValidIdAndDto_UpdatesAdvantage()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeAdvantage = new Advantage { Id = validId };
        var fakeAdvantageUpdateDTO = new AdvantageUpdateDTO();

        _mockAdvantagesReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeAdvantage);

        // Act
        await _advantageServices.UpdateAsync(validId, fakeAdvantageUpdateDTO);

        // Assert
        _mockAdvantagesWriteRepository.Verify(repo => repo.Update(fakeAdvantage), Times.Once);
        _mockAdvantagesWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_InvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();
        var fakeAdvantageUpdateDTO = new AdvantageUpdateDTO();

        _mockAdvantagesReadRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((Advantage)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _advantageServices.UpdateAsync(invalidId, fakeAdvantageUpdateDTO));

        // Verify that Update and SaveChangeAsync were not called
        _mockAdvantagesWriteRepository.Verify(repo => repo.Update(It.IsAny<Advantage>()), Times.Never);
        _mockAdvantagesWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Never);
    }


    //Create
    [Fact]
    public async Task CreateAsync_ValidAdvantageCreateDTO_CreatesAdvantage()
    {
        // Arrange
        var fakeAdvantageCreateDTO = new AdvantageCreateDTO
        {
            Title = "Title",
            Descrption = "Description",
        };

        // Act
        await _advantageServices.CreateAsync(fakeAdvantageCreateDTO);

        // Assert
        _mockAdvantagesWriteRepository.Verify(repo => repo.AddAsync(It.IsAny<Advantage>()), Times.Once);
        _mockAdvantagesWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_NullAdvantageCreateDTO_ThrowsNotFoundException()
    {
        // Arrange
        AdvantageCreateDTO nullAdvantageCreateDTO = null;

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _advantageServices.CreateAsync(nullAdvantageCreateDTO));

        // Verify that AddAsync and SaveChangeAsync were not called
        _mockAdvantagesWriteRepository.Verify(repo => repo.AddAsync(It.IsAny<Advantage>()), Times.Never);
        _mockAdvantagesWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Never);
    }


}
