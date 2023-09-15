using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;

namespace EndProjet.Persistance.Implementations.Services;

public class PickUpServices : IPickUpServices
{
    private readonly IPickupLocationReadRepository _locationReadRepository;
    private readonly IPickupLocationWriteRepository _locationWriteRepository;

    public PickUpServices(IPickupLocationReadRepository locationReadRepository,
                          IPickupLocationWriteRepository locationWriteRepository)
    {
        _locationReadRepository = locationReadRepository;
        _locationWriteRepository = locationWriteRepository;
    }

    public async Task RemoveAsync(Guid id)
    {
        var ByPickUp = await _locationReadRepository.GetByIdAsync(id);
        if (ByPickUp is not null) _locationWriteRepository.Remove(ByPickUp);
        await _locationWriteRepository.SavaChangeAsync();
    }
}
