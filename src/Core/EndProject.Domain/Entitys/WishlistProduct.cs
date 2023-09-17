using Stripe;

namespace EndProject.Domain.Entitys;

public class WishlistProduct
{
    public int Quantity { get; set; }
    public Guid WishlistId { get; set; }
    public Wishlist Wishlist { get; set; }
    public Guid CarId { get; set; }
    public Car Car { get; set; }
}
