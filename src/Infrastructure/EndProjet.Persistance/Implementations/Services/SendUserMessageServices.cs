using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarReservation;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.Implementations.Services;

public class SendUserMessageServices : ISendUserMessageServices
{
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly IUserMessageWriteRepository _userMessageWriteRepository;

    public SendUserMessageServices(IEmailService emailService,
                                   IMapper mapper,
                                   IUserMessageWriteRepository userMessageWriteRepository)
    {
        _emailService = emailService;
        _mapper = mapper;
        _userMessageWriteRepository = userMessageWriteRepository;
    }

    public async Task ByUserEmailMessage(UserEmailMessageDTO userEmailMessage)
    {
        if (userEmailMessage.Email is not null && userEmailMessage.Message is not null)
        {
            var ToEntity = _mapper.Map<SendUserMessage>(userEmailMessage);
            await _userMessageWriteRepository.AddAsync(ToEntity);
            await _userMessageWriteRepository.SavaChangeAsync();

            //string subject = "LuxeDrive Message";
            //string html = string.Empty;

            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "UserEmailMessage.html");
            //html = System.IO.File.ReadAllText(filePath);

            //html = html.Replace("{{Message}}", userEmailMessage.Message);

            //_emailService.Send(userEmailMessage.Email, subject, html);

        }
        else throw new Exception("There is no email address or message");
    }
}
