using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Faq;
using EndProjet.Persistance.Exceptions;
using EndProjet.Persistance.Implementations.Services;
using Moq;

namespace LuxeDriveUnitTest.Services.Faq;

public class FaqServicesTests
{
    private FaqServices _faqServices;
    private Mock<IFaqReadRepository> _mockFaqReadRepository;
    private Mock<IFaqWriteRepository> _mockFaqWriteRepository;
    private Mock<IMapper> _mockMapper;
    private Mock<IQRCoderServıces> _mockQRCoderServices;

    public FaqServicesTests()
    {
        _mockFaqReadRepository = new Mock<IFaqReadRepository>();
        _mockFaqWriteRepository = new Mock<IFaqWriteRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockQRCoderServices = new Mock<IQRCoderServıces>();

        _faqServices = new FaqServices(
            _mockFaqReadRepository.Object,
            _mockFaqWriteRepository.Object,
            _mockMapper.Object,
            _mockQRCoderServices.Object
        );
    }


    //GetAll

    //[Fact]
    //public async Task GetAllAsync_WithValidData_ReturnsListOfFaqGetDTO()
    //{
    //    // Arrange
    //    var fakeFaqs = new List<EndProject.Domain.Entitys.Faq> { new EndProject.Domain.Entitys.Faq(), new EndProject.Domain.Entitys.Faq(), new EndProject.Domain.Entitys.Faq() };
    //    var fakeFaqGetDTOs = fakeFaqs.Select(f => new FaqGetDTO()).ToList();

    //    _mockFaqReadRepository.Setup(repo => repo.GetAll()).Returns(fakeFaqs.AsQueryable());
    //    _mockMapper.Setup(mapper => mapper.Map<List<FaqGetDTO>>(fakeFaqs)).Returns(fakeFaqGetDTOs);

    //    // Act
    //    var result = await _faqServices.GetAllAsync();

    //    // Assert
    //    Assert.Equal(fakeFaqGetDTOs, result);
    //}

    //[Fact]
    //public async Task GetAllAsync_WithNoData_ThrowsNotFoundException()
    //{
    //    // Arrange
    //    _mockFaqReadRepository.Setup(repo => repo.GetAll()).Returns(new List<EndProject.Domain.Entitys.Faq>().AsQueryable());

    //    // Act and Assert
    //    await Assert.ThrowsAsync<NotFoundException>(() => _faqServices.GetAllAsync());
    //}




    //GetById
    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsFaqGetDTO()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeFaq = new EndProject.Domain.Entitys.Faq();
        var fakeFaqGetDTO = new FaqGetDTO();

        _mockFaqReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeFaq);
        _mockMapper.Setup(mapper => mapper.Map<FaqGetDTO>(fakeFaq)).Returns(fakeFaqGetDTO);

        // Act
        var result = await _faqServices.GetByIdAsync(validId);

        // Assert
        Assert.Equal(fakeFaqGetDTO, result);
    }

    [Fact]
    public void GetByIdAsync_InvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        _mockFaqReadRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((EndProject.Domain.Entitys.Faq)null);

        // Act and Assert
        Assert.ThrowsAsync<NotFoundException>(async () => await _faqServices.GetByIdAsync(invalidId));
    }



    //Remove
    [Fact]
    public async Task RemoveAsync_ValidId_RemovesFaq()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeFaq = new EndProject.Domain.Entitys.Faq { Id = validId };

        _mockFaqReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeFaq);

        // Act
        await _faqServices.RemoveAsync(validId);

        // Assert
        _mockFaqWriteRepository.Verify(repo => repo.Remove(fakeFaq), Times.Once);
        _mockFaqWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_InvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        _mockFaqReadRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((EndProject.Domain.Entitys.Faq)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _faqServices.RemoveAsync(invalidId));

        // Verify that Remove and SaveChangeAsync were not called
        _mockFaqWriteRepository.Verify(repo => repo.Remove(It.IsAny<EndProject.Domain.Entitys.Faq>()), Times.Never);
        _mockFaqWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Never);
    }



    //Update
    [Fact]
    public async Task UpdateAsync_ValidIdAndDto_UpdatesFaq()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeFaq = new EndProject.Domain.Entitys.Faq { Id = validId };
        var fakeFaqUpdateDTO = new FaqUpdateDTO();

        _mockFaqReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeFaq);

        // Act
        await _faqServices.UpdateAsync(validId, fakeFaqUpdateDTO);

        // Assert
        _mockFaqWriteRepository.Verify(repo => repo.Update(fakeFaq), Times.Once);
        _mockFaqWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_InvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();
        var fakeFaqUpdateDTO = new FaqUpdateDTO();

        _mockFaqReadRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((EndProject.Domain.Entitys.Faq)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _faqServices.UpdateAsync(invalidId, fakeFaqUpdateDTO));

        // Verify that Update and SaveChangeAsync were not called
        _mockFaqWriteRepository.Verify(repo => repo.Update(It.IsAny<EndProject.Domain.Entitys.Faq>()), Times.Never);
        _mockFaqWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Never);
    }


    //Create
    [Fact]
    public async Task CreateAsync_ValidData_CreatesFaq()
    {
        // Arrange
        string title = "Sample Title";
        string description = "Sample Description";

        // Act
        await _faqServices.CreateAsync(title, description);

        // Assert
        _mockFaqWriteRepository.Verify(repo => repo.AddAsync(It.IsAny<EndProject.Domain.Entitys.Faq>()), Times.Once);
        _mockFaqWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }

}
