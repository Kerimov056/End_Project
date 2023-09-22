using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Car;
using EndProject.Application.DTOs.Wishlist;
using EndProjet.Persistance.Implementations.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;

namespace LuxeDriveUnitTest.Services.Wishlist;

public class WishlistServicesTests
{
    private readonly WishlistServices _wishlistServices;
    private readonly Mock<IWishlistReadRepository> _mockWishReadRepository;
    private readonly Mock<IWishlistWriteRepository> _mockWishWriteRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IWishlistProductServices> _mockWishProductServices;
    private readonly Mock<ICarServices> _mockCarServices;


    public WishlistServicesTests()
    {
        _mockWishReadRepository = new Mock<IWishlistReadRepository>();
        _mockWishWriteRepository = new Mock<IWishlistWriteRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockWishProductServices = new Mock<IWishlistProductServices>();
        _mockCarServices = new Mock<ICarServices>();

        _wishlistServices = new WishlistServices(_mockWishReadRepository.Object, _mockWishWriteRepository.Object,
                _mockMapper.Object, _mockWishProductServices.Object, _mockCarServices.Object);
    }


    //Create
    //[Fact]
    //public async Task AddWishlistAsync_WishlistExistsAndProductNotExists_AddsProduct()
    //{
    //    // Arrange
    //    string appUserId = "sampleUserId";
    //    Guid carId = Guid.NewGuid();

    //    // Mock existing wishlist
    //    var existingWishlist = new EndProject.Domain.Entitys.Wishlist
    //    {
    //        Id = Guid.NewGuid(),
    //        AppUserId = appUserId,
    //        WishlistProducts = new List<EndProject.Domain.Entitys.WishlistProduct>()
    //    };
    //    _mockWishReadRepository.Setup(repo => repo.Table.Include(It.IsAny<Expression<Func<EndProject.Domain.Entitys.Wishlist, object>>>()))
    //                           .Returns(new List<EndProject.Domain.Entitys.Wishlist> { existingWishlist }.AsQueryable());

    //    // Mock adding a product
    //    _mockWishWriteRepository.Setup(repo => repo.AddAsync(It.IsAny<EndProject.Domain.Entitys.Wishlist>()))
    //                           .ReturnsAsync((EndProject.Domain.Entitys.Wishlist wishlist) =>
    //                           {
    //                               wishlist.Id = Guid.NewGuid();
    //                               return wishlist;
    //                           });

    //    // Mock saving changes
    //    _mockWishWriteRepository.Setup(repo => repo.SavaChangeAsync()).Returns(Task.CompletedTask);

    //    // Act
    //    await _wishlistServices.AddWishlistAsync(carId, appUserId);

    //    // Assert
    //    _mockWishWriteRepository.Verify(repo => repo.AddAsync(It.IsAny<EndProject.Domain.Entitys.Wishlist>()), Times.Never);
    //    _mockWishWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    //}

    //[Fact]
    //public async Task AddWishlistAsync_WishlistNotExists_AddsWishlistAndProduct()
    //{
    //    // Arrange
    //    string appUserId = "sampleUserId";
    //    Guid carId = Guid.NewGuid();

    //    // Mock non-existing wishlist
    //    _mockWishReadRepository.Setup(repo => repo.Table.Include(It.IsAny<Expression<Func<EndProject.Domain.Entitys.Wishlist, object>>>()))
    //                           .Returns(new List<EndProject.Domain.Entitys.Wishlist>().AsQueryable());

    //    // Mock adding a wishlist
    //    _mockWishWriteRepository.Setup(repo => repo.AddAsync(It.IsAny<EndProject.Domain.Entitys.Wishlist>()))
    //                           .ReturnsAsync((EndProject.Domain.Entitys.Wishlist wishlist) =>
    //                           {
    //                               wishlist.Id = Guid.NewGuid();
    //                               return wishlist;
    //                           });

    //    // Mock saving changes
    //    _mockWishWriteRepository.Setup(repo => repo.SavaChangeAsync()).Returns(Task.CompletedTask);

    //    // Act
    //    await _wishlistServices.AddWishlistAsync(carId, appUserId);

    //    // Assert
    //    _mockWishWriteRepository.Verify(repo => repo.AddAsync(It.IsAny<EndProject.Domain.Entitys.Wishlist>()), Times.Once);
    //    _mockWishWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    //}


    //[Fact]
    //public async Task GetWishlistProductsAsync_WithValidAppUserId_ReturnsListOfWishlistProductDto()
    //{
    //    // Arrange
    //    string appUserId = "sampleUserId";

    //    // Mock Wishlist and WishlistProducts
    //    var wishlist = new EndProject.Domain.Entitys.Wishlist
    //    {
    //        Id = Guid.NewGuid(),
    //        AppUserId = appUserId,
    //        WishlistProducts = new List<EndProject.Domain.Entitys.WishlistProduct>
    //    {
    //        new EndProject.Domain.Entitys.WishlistProduct { CarId = Guid.NewGuid() },
    //        new EndProject.Domain.Entitys.WishlistProduct { CarId = Guid.NewGuid() },
    //        new EndProject.Domain.Entitys.WishlistProduct { CarId = Guid.NewGuid() }
    //    }
    //    };
    //    _mockWishReadRepository.Setup(repo => repo.Table.Include(It.IsAny<Expression<Func<EndProject.Domain.Entitys.Wishlist, object>>>()))
    //                           .Returns(new List<EndProject.Domain.Entitys.Wishlist> { wishlist }.AsQueryable());

    //    // Mock CarServices
    //    var carGetDtos = wishlist.WishlistProducts.Select(wp => new CarGetDTO { Id = wp.CarId }).ToList();
    //    _mockCarServices.Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
    //                    .ReturnsAsync((Guid carId) => carGetDtos.FirstOrDefault(dto => dto.Id == carId));

    //    // Act
    //    var result = await _wishlistServices.GetWishlistProductsAsync(appUserId);

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.IsType<List<WishlistProductDto>>(result);
    //    Assert.Equal(wishlist.WishlistProducts.Count, result.Count);

    //    foreach (var wishlistProductDto in result)
    //    {
    //        Assert.NotNull(wishlistProductDto.carGetDTO);
    //        Assert.Equal(wishlist.WishlistProducts.FirstOrDefault(wp => wp.CarId == wishlistProductDto.CarId).CarId, wishlistProductDto.carGetDTO.Id);
    //    }
    //}

    //[Fact]
    //public async Task GetWishlistProductsAsync_WithInvalidAppUserId_ThrowsNullReferenceException()
    //{
    //    // Arrange
    //    string appUserId = "invalidUserId";

    //    // Mock Wishlist (empty)
    //    _mockWishReadRepository.Setup(repo => repo.Table.Include(It.IsAny<Expression<Func<EndProject.Domain.Entitys.Wishlist, object>>>()))
    //                           .Returns(new List<EndProject.Domain.Entitys.Wishlist>().AsQueryable());

    //    // Act and Assert
    //    await Assert.ThrowsAsync<NullReferenceException>(() => _wishlistServices.GetWishlistProductsAsync(appUserId));
    //}

}
