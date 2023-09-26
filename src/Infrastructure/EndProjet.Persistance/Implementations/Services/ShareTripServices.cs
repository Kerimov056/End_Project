using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.ShareTrip;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Identity;
using EndProject.Domain.Enums.Role;
using EndProjet.Persistance.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class ShareTripServices : IShareTripServices
{
    private readonly IShareTripReadRepository _readRepository;
    private readonly IShareTripWriteRepository _writeRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ITripeReadRepository _tripeReadRepository;

    public ShareTripServices(IShareTripReadRepository readRepository,
                             IShareTripWriteRepository writeRepository,
                             IMapper mapper,
                             UserManager<AppUser> userManager,
                             IEmailService emailService,
                             ITripeReadRepository tripeReadRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
        _userManager = userManager;
        _emailService = emailService;
        _tripeReadRepository = tripeReadRepository;
    }

    public async Task<bool> AccesTripNote(Guid tripId, string AppUserId)
    {
        var shareTrip = _readRepository.GetAll()
                        .Where(x => x.AppUserId == AppUserId)
                        .Where(x => x.TripId == tripId)
                        .Where(x => x.TripRole == TripRole.Contributor)
                        .FirstOrDefault();
        if (shareTrip is null) return false;
        return true;
    }

    public async Task CreateAsync(ShareTripCreateDTO shareTripCreateDTO)
    {
        var myTrip = await _tripeReadRepository
            .GetByIdAsyncExpression(x => x.Id == shareTripCreateDTO.TripId &&
             x.AppUserId == shareTripCreateDTO.AppUserId);
        if (myTrip is null)
        {
            var byShareUser = await _userManager.FindByIdAsync(shareTripCreateDTO.AppUserId);
            if (byShareUser is null) throw new NotFoundException("User not Found");

            var byContributor = await _readRepository
                .GetByIdAsyncExpression(x => x.Email == byShareUser.Email &&
                                        x.TripRole == TripRole.Contributor);

            if (byContributor is null) throw new Exception("No Access");
        }

        var shareTrip = await _readRepository
        .GetByIdAsyncExpression(x => x.Email == shareTripCreateDTO.Email &&
                                x.TripId == shareTripCreateDTO.TripId);
        if (shareTrip is not null)
        {
            var byGuestShare = await _userManager.FindByEmailAsync(shareTrip.Email);
            if (byGuestShare is null)
            {
                var toUpdate = _mapper.Map<ShareTripUpdateDTO>(shareTripCreateDTO);
                toUpdate.TripRole = TripRole.Guest;
                await UpdateAsync(shareTrip.Id, toUpdate);
            }
            else
            {
                var toUpdate = _mapper.Map<ShareTripUpdateDTO>(shareTripCreateDTO);
                await UpdateAsync(shareTrip.Id, toUpdate);
            }
            return;
        }

        var byGuest = await _userManager.FindByEmailAsync(shareTripCreateDTO.Email);
        if (byGuest is null)
        {
            var GuestShareTrip = _mapper.Map<ShareTrip>(shareTripCreateDTO);
            GuestShareTrip.TripRole = TripRole.Guest;
            await _writeRepository.AddAsync(GuestShareTrip);
            await _writeRepository.SavaChangeAsync();

            var ByTrip = await _tripeReadRepository.GetByIdAsync(shareTripCreateDTO.TripId);

            string subject = "There is a new reservation";
            string html = string.Empty;


            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "TripAddUser.html");
            html = System.IO.File.ReadAllText(filePath);

            var LinkTrip = $"https://localhost:7152/api/Car/qrcodeImage?id={shareTripCreateDTO.TripId}";
            var message = $"{shareTripCreateDTO.Message}";

            html = html.Replace("{{LinkTrip}}", LinkTrip);
            html = html.Replace("{{Message}}", message);
           

            _emailService.Send(shareTripCreateDTO.Email, subject, html);

            return;
        }




        string subject1 = "There is a new reservation";
        string html1 = string.Empty;


        string filePath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "TripAddUser.html");
        html1 = System.IO.File.ReadAllText(filePath1);

        var LinkTrip1 = $"https://localhost:7152/api/Car/qrcodeImage?id={shareTripCreateDTO.TripId}";
        var message1 = $"{shareTripCreateDTO.Message}";


        html1 = html1.Replace("{{LinkTrip}}", LinkTrip1);
        html1 = html1.Replace("{{Message}}", message1);


        _emailService.Send(shareTripCreateDTO.Email, subject1, html1);


        var newShareTrip = _mapper.Map<ShareTrip>(shareTripCreateDTO);
        await _writeRepository.AddAsync(newShareTrip);
        await _writeRepository.SavaChangeAsync();
    }

    public async Task<List<ShareTripGetDTO>> GetAllAsync(Guid tripId)
    {
        var byTrip = await _tripeReadRepository.GetByIdAsync(tripId);
        if (byTrip is null) throw new NotFoundException("Trip Not Found");

        var allShereUser = await _readRepository.GetAll().ToListAsync();
        var toDto = _mapper.Map<List<ShareTripGetDTO>>(allShereUser);

        return toDto;
    }

    public async Task<List<ShareTripGetDTO>> GetAllContributorsUser(Guid tripId)
    {
        var byTrip = await _tripeReadRepository.GetByIdAsync(tripId);
        if (byTrip is null) throw new NotFoundException("Trip Not Found");

        var allShereUser = await _readRepository.GetAll()
            .Where(x => x.TripRole == TripRole.Contributor).ToListAsync();

        var toDto = _mapper.Map<List<ShareTripGetDTO>>(allShereUser);
        return toDto;
    }

    public async Task<ShareTripGetDTO> GetByIdAsync(Guid Id)
    {
        var byShare = await _readRepository.GetByIdAsync(Id);
        if (byShare is null) throw new NotFoundException("Not Found");
        var toDto = _mapper.Map<ShareTripGetDTO>(byShare);
        return toDto;
    }

    public async Task RemoveAsync(Guid id)
    {
        var byShare = await _readRepository.GetByIdAsync(id);
        if (byShare is null) throw new NotFoundException("Not Found");

        _writeRepository.Remove(byShare);
        await _writeRepository.SavaChangeAsync();
    }

    public async Task RemoveRangeAsync(Guid tripId)
    {
        var allShareTrip = await _readRepository.GetAll()
                            .Where(x => x.TripId == tripId)
                            .ToListAsync();
        if (allShareTrip is null) return;

        _writeRepository.RemoveRange(allShareTrip);
        await _writeRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid id, ShareTripUpdateDTO shareTripUpdateDTO)
    {
        var byShare = await _readRepository.GetByIdAsync(id);
        if (byShare is null) throw new NotFoundException("Not Found");

        _mapper.Map(shareTripUpdateDTO, byShare);
        _writeRepository.Update(byShare);
        await _writeRepository.SavaChangeAsync();


        string subject = "There is a new reservation";
        string html = string.Empty;


        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "TripAddUser.html");
        html = System.IO.File.ReadAllText(filePath);

        var LinkTrip = $"https://localhost:7152/api/Car/qrcodeImage?id={shareTripUpdateDTO.TripId}";
        var message1 = $"{shareTripUpdateDTO.Message}";


        html = html.Replace("{{LinkTrip}}", LinkTrip);
        html = html.Replace("{{Message}}", message1);


        _emailService.Send(shareTripUpdateDTO.Email, subject, html);
    }
}
