using EndProject.Application.DTOs.Car;

namespace EndProject.Application.DTOs.Basket;

public class BasketProductListDto
{
    public int Quantity { get; set; }
    public Guid BasketId { get; set; }
    public Guid CarId { get; set; }
    public CarGetDTO carGetDTO { get; set; }
}
