using EndProject.Application.DTOs.Advantage;

namespace EndProject.Application.Abstraction.Services;

public interface IAdvantageServices
{
    Task<List<AdvantageGetDTO>> GetAllAsync();
    Task CreateAsync(AdvantageCreateDTO advantageCreateDTO);
    Task<AdvantageGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, AdvantageUpdateDTO advantageUpdateDTO);
    Task RemoveAsync(Guid id);

    //QRCoder
    Task<byte[]> GetGRCodeByIdAsync(Guid Id);
}
