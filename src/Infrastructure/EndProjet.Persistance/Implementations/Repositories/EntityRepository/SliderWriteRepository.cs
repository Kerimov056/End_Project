using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class SliderWriteRepository : WriteRepository<Slider>, ISliderWriteRepository
{
    public SliderWriteRepository(AppDbContext context) : base(context)
    {
    }
}