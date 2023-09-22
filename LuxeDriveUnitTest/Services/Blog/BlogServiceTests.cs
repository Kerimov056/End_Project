using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProjet.Persistance.Exceptions;
using EndProjet.Persistance.Implementations.Services;
using Moq;

namespace LuxeDriveUnitTest.Services.Blog;

public class BlogServiceTests
{
    private readonly BlogService _blogService;
    private readonly Mock<IBlogReadRepository> _mockBlogReadRepository;
    private readonly Mock<IBlogWriteRepository> _mockBlogWriteRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IBlogImageServices> _mockBlogImageServices;


    public BlogServiceTests()
    {
        _mockBlogReadRepository = new Mock<IBlogReadRepository>();
        _mockBlogWriteRepository = new Mock<IBlogWriteRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockBlogImageServices = new Mock<IBlogImageServices>();

        _blogService = new BlogService(_mockBlogReadRepository.Object, _mockBlogWriteRepository.Object,
                _mockMapper.Object, _mockBlogImageServices.Object);
    }



    //GetById
    //[Fact]
    //public async Task GetByIdAsync_WithValidId_ReturnsBlogGetDTO()
    //{
    //    // Arrange
    //    Guid validId = Guid.NewGuid();
    //    var fakeBlog = new EndProject.Domain.Entitys.Blog { Id = validId, BlogImages = new List<BlogImage> { new BlogImage() } };
    //    var fakeBlogGetDTO = new BlogGetDTO { Id = validId };

    //    _mockBlogReadRepository.Setup(repo => repo.GetAll().Include(It.IsAny<Expression<Func<EndProject.Domain.Entitys.Blog, object>>>()))
    //                           .Returns(new List<EndProject.Domain.Entitys.Blog> { fakeBlog }.AsQueryable());

    //    _mockMapper.Setup(mapper => mapper.Map<BlogGetDTO>(fakeBlog)).Returns(fakeBlogGetDTO);
    //    _mockBlogImageServices.Setup(services => services.GetAllBlogIdAsync(validId))
    //                          .ReturnsAsync(new List<BlogImageDTO> { new BlogImageDTO() });

    //    // Act
    //    var result = await _blogService.GetByIdAsync(validId);

    //    // Assert
    //    Assert.Equal(fakeBlogGetDTO, result);
    //}

    //[Fact]
    //public async Task GetByIdAsync_WithInvalidId_ThrowsNotFoundException()
    //{
    //    // Arrange
    //    Guid invalidId = Guid.NewGuid();

    //    _mockBlogReadRepository.Setup(repo => repo.GetAll().Include(It.IsAny<Expression<Func<EndProject.Domain.Entitys.Blog, object>>>()))
    //                           .Returns(new List<EndProject.Domain.Entitys.Blog>().AsQueryable());

    //    // Act and Assert
    //    await Assert.ThrowsAsync<NotFoundException>(() => _blogService.GetByIdAsync(invalidId));
    //}

    //Remove
    [Fact]
    public async Task RemoveAsync_ValidId_RemovesBlog()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var fakeBlog = new EndProject.Domain.Entitys.Blog { Id = validId };

        _mockBlogReadRepository.Setup(repo => repo.GetByIdAsync(validId))
                               .ReturnsAsync(fakeBlog);

        // Act
        await _blogService.RemoveAsync(validId);

        // Assert
        _mockBlogWriteRepository.Verify(repo => repo.Remove(fakeBlog), Times.Once);
        _mockBlogWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_InvalidId_ThrowsNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        _mockBlogReadRepository.Setup(repo => repo.GetByIdAsync(invalidId))
                               .ReturnsAsync((EndProject.Domain.Entitys.Blog)null);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _blogService.RemoveAsync(invalidId));

        // Verify that Remove and SaveChangeAsync were not called
        _mockBlogWriteRepository.Verify(repo => repo.Remove(It.IsAny<EndProject.Domain.Entitys.Blog>()), Times.Never);
        _mockBlogWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Never);
    }



    ////Create
    //[Fact]
    //public async Task CreateAsync_ValidData_CreatesBlogAndImages()
    //{
    //    // Arrange
    //    var newBlog = new EndProject.Domain.Entitys.Blog
    //    {
    //        Title = "Sample Title",
    //        Description = "Sample Description"
    //    };

    //    // Set up AddAsync to return the newly created blog
    //    _mockBlogWriteRepository.Setup(repo => repo.AddAsync(It.IsAny<EndProject.Domain.Entitys.Blog>()))
    //                           .ReturnsAsync(newBlog);

    //    // Act
    //    await _blogService.CreateAsync(newBlog);

    //    // Assert
    //    _mockBlogWriteRepository.Verify(repo => repo.AddAsync(It.IsAny<EndProject.Domain.Entitys.Blog>()), Times.Once);
    //    _mockBlogWriteRepository.Verify(repo => repo.SavaChangeAsync(), Times.Once);

    //    // Verify that CreateAsync is called for each blog image
    //    foreach (var imagePath in newBlog.blogImages)
    //    {
    //        var newBlogImage = new BlogImageCreateDTO
    //        {
    //            BlogId = newBlog.Id,
    //            imagePath = imagePath
    //        };
    //        _mockBlogImageServices.Verify(services => services.CreateAsync(newBlogImage), Times.Once);
    //    }
    //}


}
