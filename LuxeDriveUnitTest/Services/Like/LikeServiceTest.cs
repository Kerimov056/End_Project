using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.DTOs.Like;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;
using EndProjet.Persistance.Exceptions;
using EndProjet.Persistance.Implementations.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;

namespace LuxeDriveUnitTest.Services.Like;

public class LikeServiceTest
{
    private readonly LikeServices _likeServices;
    private readonly Mock<ILikeReadRepository> _mockLikeReadRepository;
    private readonly Mock<ILikeWriteRepository> _mockLikeWriteRepository;
    private readonly Mock<ICarCommentWriteRepository> _mockCarCommentWriteRepository;
    private readonly Mock<AppDbContext> _mockAppDbContext;
    private readonly Mock<IMapper> _mockMapper;

    public LikeServiceTest()
    {
        _mockLikeReadRepository = new Mock<ILikeReadRepository>();
        _mockLikeWriteRepository = new Mock<ILikeWriteRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockCarCommentWriteRepository = new Mock<ICarCommentWriteRepository>();
        _mockAppDbContext = new Mock<AppDbContext>();

        _likeServices = new LikeServices(_mockLikeReadRepository.Object, _mockLikeWriteRepository.Object,
              _mockMapper.Object, _mockCarCommentWriteRepository.Object, _mockAppDbContext.Object);
    }


    //[Fact]
    //public async Task GetAllAsync_WithValidData_ReturnsListOfLikeGetDTO()
    //{
    //    // Arrange
    //    var fakeLikes = new List<EndProject.Domain.Entitys.Like> 
    //    { new EndProject.Domain.Entitys.Like(), 
    //        new EndProject.Domain.Entitys.Like(), 
    //          new EndProject.Domain.Entitys.Like() };

    //    var fakeLikeGetDTOs = fakeLikes.Select(l => new LikeGetDTO()).ToList();

    //    _mockLikeReadRepository.Setup(repo => repo.GetAll()).Returns(fakeLikes.AsQueryable());
    //    _mockMapper.Setup(mapper => mapper.Map<List<LikeGetDTO>>(fakeLikes)).Returns(fakeLikeGetDTOs);

    //    // Act
    //    var result = await _likeServices.GetAllAsync();

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.IsType<List<LikeGetDTO>>(result);
    //    Assert.Equal(fakeLikeGetDTOs.Count, result.Count);
    //    Assert.Equal(fakeLikeGetDTOs, result);
    //}

    //[Fact]
    //public async Task GetAllAsync_WithNoData_ReturnsEmptyList()
    //{
    //    // Arrange
    //    _mockLikeReadRepository.Setup(repo => repo.GetAll()).Returns(new List<EndProject.Domain.Entitys.Like>().AsQueryable());

    //    // Act
    //    var result = await _likeServices.GetAllAsync();

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.IsType<List<LikeGetDTO>>(result);
    //    Assert.Empty(result);
    //}

    //[Fact]
    //public async Task GetByIdAsync_WithValidId_ReturnsLikeGetDTO()
    //{
    //    // Arrange
    //    Guid validId = Guid.NewGuid();
    //    var fakeLike = new EndProject.Domain.Entitys.Like { Id = validId };
    //    var fakeLikeGetDTO = new LikeGetDTO { Id = validId };

    //    _mockLikeReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeLike);
    //    _mockMapper.Setup(mapper => mapper.Map<LikeGetDTO>(fakeLike)).Returns(fakeLikeGetDTO);

    //    // Act
    //    var result = await _likeServices.GetByIdAsync(validId);

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.IsType<LikeGetDTO>(result);
    //    Assert.Equal(validId, result.Id);
    //}

    //[Fact]
    //public async Task GetByIdAsync_WithInvalidId_ThrowsNotFoundException()
    //{
    //    // Arrange
    //    Guid invalidId = Guid.NewGuid();

    //    _mockLikeReadRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((EndProject.Domain.Entitys.Like)null);

    //    // Act and Assert
    //    await Assert.ThrowsAsync<NotFoundException>(() => _likeServices.GetByIdAsync(invalidId));
    //}

    //[Fact]
    //public async Task RemoveAsync_WithValidId_RemovesLikeAndSavesChanges()
    //{
    //    // Arrange
    //    Guid validId = Guid.NewGuid();
    //    var fakeLike = new EndProject.Domain.Entitys.Like { Id = validId };

    //    _mockLikeReadRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeLike);

    //    // Act
    //    await _likeServices.RemoveAsync(validId);

    //    // Assert
    //    _mockLikeWriteRepository.Verify(repo => repo.Remove(fakeLike), Times.Once);
    //    _mockLikeWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    //}

    //[Fact]
    //public async Task RemoveAsync_WithInvalidId_DoesNotRemoveLike()
    //{
    //    // Arrange
    //    Guid invalidId = Guid.NewGuid();

    //    _mockLikeReadRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((EndProject.Domain.Entitys.Like)null);

    //    // Act
    //    await _likeServices.RemoveAsync(invalidId);

    //    // Assert
    //    _mockLikeWriteRepository.Verify(repo => repo.Remove(It.IsAny<EndProject.Domain.Entitys.Like>()), Times.Never);
    //    _mockLikeWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Never);
    //}


    //[Fact]
    //public async Task CreateAsync_NewLike_CreatesNewLikeAndSavesChanges()
    //{
    //    // Arrange
    //    string userId = "user123";
    //    Guid carCommentId = Guid.NewGuid();
    //    var fakeLike = new EndProject.Domain.Entitys.Like { AppUserId = userId, CarCommentId = carCommentId };

    //    _mockLikeReadRepository.Setup(repo => repo
    //        .GetAll()
    //        .Where(It.IsAny<Expression<Func<EndProject.Domain.Entitys.Like, bool>>>())
    //        .FirstOrDefaultAsync())
    //        .ReturnsAsync((EndProject.Domain.Entitys.Like)null);

    //    _mockAppDbContext.Setup(context => context.CarComments.FindAsync(carCommentId))
    //        .ReturnsAsync(new CarComment { Id = carCommentId });

    //    // Act
    //    await _likeServices.CreateAsync(userId, carCommentId);

    //    // Assert
    //    _mockLikeWriteRepository.Verify(repo => repo.AddAsync(It.IsAny<EndProject.Domain.Entitys.Like>()), Times.Once);
    //    _mockCarCommentWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    //    _mockLikeWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    //}

    //[Fact]
    //public async Task CreateAsync_ExistingLike_RemovesLikeAndSavesChanges()
    //{
    //    // Arrange
    //    string userId = "user123";
    //    Guid carCommentId = Guid.NewGuid();

    //    _mockLikeReadRepository.Setup(repo => repo
    //        .GetAll()
    //        .Where(It.IsAny<Expression<Func<EndProject.Domain.Entitys.Like, bool>>>())
    //        .FirstOrDefaultAsync())
    //        .ReturnsAsync(new EndProject.Domain.Entitys.Like());

    //    _mockAppDbContext.Setup(context => context.CarComments.FindAsync(carCommentId))
    //        .ReturnsAsync(new CarComment { Id = carCommentId });

    //    // Act
    //    await _likeServices.CreateAsync(userId, carCommentId);

    //    // Assert
    //    _mockLikeWriteRepository.Verify(repo => repo.Remove(It.IsAny<EndProject.Domain.Entitys.Like>()), Times.Once);
    //    _mockCarCommentWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    //    _mockLikeWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    //}

}
