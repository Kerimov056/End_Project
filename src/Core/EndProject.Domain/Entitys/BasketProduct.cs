using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class BasketProduct : BaseEntity
{
    public int Quantity { get; set; }
    public Guid BasketId { get; set; }
    public Basket Basket { get; set; }
    public Guid CarId { get; set; }
    public Car Car { get; set; }
}
