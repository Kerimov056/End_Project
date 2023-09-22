using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProjet.Persistance.Implementations.Services;
using Moq;

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

}
