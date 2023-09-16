using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CampaignStatistika;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services
{
    public class CampaignStatistikaServices : ICampaignStatistikaServices
    {
        private readonly ICampaignStatistikaReadRepository _readRepository;
        private readonly ICampaignStatistikaWriteRepository _writeRepository;
        private readonly IMapper _mapper;

        public CampaignStatistikaServices(ICampaignStatistikaReadRepository readRepository,
                                          ICampaignStatistikaWriteRepository writeRepository,
                                          IMapper mapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CampaignStatistikaCreateDTO campaignStatistikaCreateDTO)
        {
            var CamaignStatiska = _mapper.Map<CampaignStatistika>(campaignStatistikaCreateDTO);
            CamaignStatiska.ReservationSum = 1;
            await _writeRepository.AddAsync(CamaignStatiska);
            await _writeRepository.SavaChangeAsync();
        }

        public async Task<List<CampaignStatistikaGetDTO>> GetAllAsync()
        {
            var allCamaignStatiska = await _readRepository
                .GetAll().OrderByDescending(x => x.CreatedDate)
                .OrderByDescending(x => x.CreatedDate).ToListAsync();
            var toDto = _mapper.Map<List<CampaignStatistikaGetDTO>>(allCamaignStatiska);
            return toDto;
        }

        public async Task<CampaignStatistikaGetDTO> GetByIdAsync(Guid Id)
        {
            var byCamaignStatiska = await _readRepository.GetByIdAsync(Id);
            if (byCamaignStatiska is null) throw new NotFoundException("CamaignStatiska is null");

            var toDto = _mapper.Map<CampaignStatistikaGetDTO>(byCamaignStatiska);
            return toDto;
        }

        public async Task RemoveAsync(Guid id)
        {
            var byCamaignStatiska = await _readRepository.GetByIdAsync(id);
            if (byCamaignStatiska is null) throw new NotFoundException("CamaignStatiska is null");

            _writeRepository.Remove(byCamaignStatiska);
            await _writeRepository.SavaChangeAsync();
        }

        public async Task UpdateAsync(Guid id)
        {
            var byCamaignStatiska = await _readRepository.GetByIdAsync(id);
            if (byCamaignStatiska is null) throw new NotFoundException("CamaignStatiska is null");

            byCamaignStatiska.ReservationSum = byCamaignStatiska.ReservationSum + 1;

            _writeRepository.Update(byCamaignStatiska);
            await _writeRepository.SavaChangeAsync();
        }
    }
}
