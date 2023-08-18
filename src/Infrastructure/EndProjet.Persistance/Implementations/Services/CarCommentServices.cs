using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarComment;

namespace EndProjet.Persistance.Implementations.Services;

public class CarCommentServices : ICarCommentServices
{
    private readonly ICarCommentReadRepository _carCommentReadRepository;
    private readonly ICarCommentWriteRepository _carCommentWriteRepository;
    private readonly IMapper _mapper;

    public CarCommentServices(ICarCommentReadRepository carCommentReadRepository,
                              ICarCommentWriteRepository carCommentWriteRepository,
                              IMapper mapper)
    {
        _carCommentReadRepository = carCommentReadRepository;
        _carCommentWriteRepository = carCommentWriteRepository;
        _mapper = mapper;
    }

    public Task CreateAsync(CarCommentCreateDTO carCommentCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<List<CarCommentGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CarCommentGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, CarCommentUpdateDTO carCommentUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
