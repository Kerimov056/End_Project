using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.DTOs.Chauffeurs;
using EndProjet.Persistance.Exceptions;
using EndProjet.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Text;

namespace LuxeDriveUnitTest.Services.Chauffeurs;

public class ChauffeursServicesTests
{
    private readonly ChauffeursServices _chauffeursService;
    private readonly Mock<IChauffeursReadRepository> _mockChauffeursReadRepository;
    private readonly Mock<IChauffeursWriteRepository> _mockChauffeursWriteRepository;
    private readonly Mock<IMapper> _mockMapper;

    public ChauffeursServicesTests()
    {
        _mockChauffeursReadRepository = new Mock<IChauffeursReadRepository>();
        _mockChauffeursWriteRepository = new Mock<IChauffeursWriteRepository>();
        _mockMapper = new Mock<IMapper>();

        _chauffeursService = new ChauffeursServices(_mockChauffeursReadRepository.Object, _mockChauffeursWriteRepository.Object,
              _mockMapper.Object);
    }


    //[Fact]
    //public async Task GetByIdAsync_WithValidId_ReturnsChauffeursGetDTO()
    //{
    //    // Arrange
    //    Guid validId = Guid.NewGuid();
    //    var fakeChauffeurs = new EndProject.Domain.Entitys.Chauffeurs
    //    {
    //        Id = validId,
    //        Name = "asd",
    //        Number = "asdas",
    //        Price = 23,
    //        isChauffeurs = false,
    //        imagePath = new byte[] { 1, 2, 23, 3 } // Example byte array
    //    };

    //    _mockChauffeursReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeChauffeurs);

    //    // Act
    //    var result = await _chauffeursService.GetByIdAsync(validId);

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.Equal("asd", result.Name);
    //    Assert.Equal("asdas", result.Number);
    //    Assert.Equal(23, result.Price);
    //    Assert.False(result.isChauffeurs);
    //    Assert.Equal("AQIWMw==", result.ImagePath); // Base64 encoded value of the example byte array
    //}


    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        _mockChauffeursReadRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((EndProject.Domain.Entitys.Chauffeurs)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _chauffeursService.GetByIdAsync(invalidId));
    }

    [Fact]
    public async Task RemoveAsync_WithValidId_RemovesChauffeurs()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeChauffeurs = new EndProject.Domain.Entitys.Chauffeurs { Id = validId, Name = "asd", Number = "asdas", Price = 23, isChauffeurs = false };

        _mockChauffeursReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeChauffeurs);

        // Act
        await _chauffeursService.RemoveAsync(validId);

        // Assert
        _mockChauffeursWriteRepository.Verify(repo => repo.Remove(fakeChauffeurs), Times.Once);
        _mockChauffeursWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_WithInvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        _mockChauffeursReadRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((EndProject.Domain.Entitys.Chauffeurs)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _chauffeursService.RemoveAsync(invalidId));
    }



    [Fact]
    public async Task IsChauffeursTrue_WithValidId_UpdatesIsChauffeursToTrue()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeChauffeurs = new EndProject.Domain.Entitys.Chauffeurs
        {
            Id = validId,
            isChauffeurs = false, 
            Name = "Test",
            Number = "233",
            Price = 123
        };

        _mockChauffeursReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeChauffeurs);

        // Act
        await _chauffeursService.IsChauffeursTrue(validId);

        // Assert
        Assert.True(fakeChauffeurs.isChauffeurs);
        _mockChauffeursWriteRepository.Verify(repo => repo.Update(fakeChauffeurs), Times.Once);
        _mockChauffeursWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }

    [Fact]
    public async Task IsChauffeursFalse_WithValidId_UpdatesIsChauffeursToFalse()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeChauffeurs = new EndProject.Domain.Entitys.Chauffeurs
        {
            Id = validId,
            isChauffeurs = true,
            Name = "Test",
            Number = "233",
            Price = 123
        };

        _mockChauffeursReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeChauffeurs);

        // Act
        await _chauffeursService.IsChauffeursFalse(validId);

        // Assert
        Assert.False(fakeChauffeurs.isChauffeurs);
        _mockChauffeursWriteRepository.Verify(repo => repo.Update(fakeChauffeurs), Times.Once);
        _mockChauffeursWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }



    //[Fact]
    //public async Task UpdateAsync_WithValidId_UpdatesChauffeur()
    //{
    //    // Arrange
    //    Guid validId = Guid.NewGuid();
    //    var fakeUpdateDTO = new ChauffeursUpdateDTO
    //    {
    //        // Set properties for the update DTO
    //        Image = new FormFile(null, 0, 0, "image", "test.jpg")
    //    };

    //    var fakeChauffeur = new EndProject.Domain.Entitys.Chauffeurs
    //    {
    //        Id = validId,
    //        Name = "Test",
    //        Number = "231",
    //        Price = 232,
    //        imagePath = Encoding.UTF8.GetBytes("Fake Image Data")
    //        // Set other properties here
    //    };
    //    _mockChauffeursReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeChauffeur);

    //    // Act
    //    await _chauffeursService.UpdateAsync(validId, fakeUpdateDTO);

    //    // Assert
    //    _mockMapper.Verify(mapper => mapper.Map(fakeUpdateDTO, fakeChauffeur), Times.Once);
    //    if (fakeUpdateDTO.Image is not null)
    //    {
    //        // Assert that the image path is updated correctly
    //        // This will depend on your specific implementation
    //        // You may need to adjust this part accordingly
    //        // Assuming you have a method like GetBytes for ImageDTO
    //        Assert.NotNull(fakeChauffeur.imagePath);
    //    }
    //    else
    //    {
    //        Assert.Null(fakeChauffeur.imagePath);
    //    }
    //    _mockChauffeursWriteRepository.Verify(repo => repo.Update(fakeChauffeur), Times.Once);
    //    _mockChauffeursWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    //}

    [Fact]
    public async Task UpdateAsync_WithInvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        _mockChauffeursReadRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((EndProject.Domain.Entitys.Chauffeurs)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _chauffeursService.UpdateAsync(invalidId, It.IsAny<ChauffeursUpdateDTO>()));
    }

}
