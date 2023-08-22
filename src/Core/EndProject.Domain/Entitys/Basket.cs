using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Domain.Entitys;

public class Basket : BaseEntity
{
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public List<BasketProduct> basketProduct { get; set; }
}
