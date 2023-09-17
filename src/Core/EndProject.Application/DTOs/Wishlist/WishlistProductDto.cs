using EndProject.Application.DTOs.Car;

namespace EndProject.Application.DTOs.Wishlist;

public class WishlistProductDto
{
    public int Quantity { get; set; }
    public Guid WishlistId { get; set; }
    public Guid CarId { get; set; }
    public CarGetDTO carGetDTO { get; set; }
}
