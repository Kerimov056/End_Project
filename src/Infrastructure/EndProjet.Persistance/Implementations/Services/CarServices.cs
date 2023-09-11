using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Car;
using EndProject.Application.DTOs.CarComment;
using EndProject.Application.DTOs.CarImage;
using EndProject.Application.DTOs.CarType;
using EndProject.Application.DTOs.Faq;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Identity;
using EndProject.Domain.Enums.CampaignsStatus;
using EndProjet.Persistance.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EndProjet.Persistance.Implementations.Services;

public class CarServices : ICarServices
{
    private readonly ICarReadRepository _carReadRepository;
    private readonly ICarWriteRepository _carWriteRepository;
    private readonly ICarTypeService _carTypeService;
    private readonly ICarTypeWriteRepository _carTypeWriteRepository;
    private readonly ICarImageServices _carImageServices;
    private readonly IMapper _mapper;
    private readonly ICarCategoryWriteRepository _carCategoryWriteRepository;
    private readonly ITagReadRepository _tagReadRepository;
    private readonly ITagWriteRepository _tagWriteRepository;
    private readonly ICarTagWriteRepository _carTagWriteRepository;
    private readonly ICarTagReadRepository _carTagReadRepository;
    private readonly ICarCategoryServices _carCategoryServices;
    private readonly ICarReservationReadRepository _carReservationReadRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;
    public CarServices(ICarReadRepository carReadRepository,
                       ICarWriteRepository carWriteRepository,
                       IMapper mapper,
                       ICarTypeService carTypeService,
                       ICarTypeWriteRepository carTypeWriteRepository,
                       ICarImageServices carImageServices,
                       ITagReadRepository tagReadRepository,
                       ITagWriteRepository tagWriteRepository,
                       ICarCategoryWriteRepository carCategoryWriteRepository,
                       ICarTagWriteRepository carTagWriteRepository,
                       ICarTagReadRepository carTagReadRepository,
                       ICarReservationReadRepository carReservationReadRepository,
                       UserManager<AppUser> userManager,
                       IEmailService emailService)
    {
        _carReadRepository = carReadRepository;
        _carWriteRepository = carWriteRepository;
        _mapper = mapper;
        _carTypeService = carTypeService;
        _carTypeWriteRepository = carTypeWriteRepository;
        _carImageServices = carImageServices;
        _tagReadRepository = tagReadRepository;
        _tagWriteRepository = tagWriteRepository;
        _carCategoryWriteRepository = carCategoryWriteRepository;
        _carTagWriteRepository = carTagWriteRepository;
        _carTagReadRepository = carTagReadRepository;
        _carReservationReadRepository = carReservationReadRepository;
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task Campaigns(CarCampaignsDTO carCampaignsDTO)
    {
        var bySuperAdmin = await _userManager.FindByIdAsync(carCampaignsDTO.SuperAdminId);
        if (bySuperAdmin is null) throw new NotFoundException("SuperAdmin Not found");
        if (bySuperAdmin == null || !await _userManager.IsInRoleAsync(bySuperAdmin, "SuperAdmin"))
            throw new UnauthorizedAccessException("You do not have permission to perform this action.");

        var allCar = await _carReadRepository.GetAll().ToListAsync();
        if (carCampaignsDTO.CampaignsInterest < 100)
        {
            var IsCompany = 100 - carCampaignsDTO.CampaignsInterest;
            if (DateTime.Now < carCampaignsDTO.PickUpCampaigns && carCampaignsDTO.ReturnCampaigns > carCampaignsDTO.PickUpCampaigns)
            {
                foreach (var item in allCar)
                {
                    item.Status = CampaignsStatus.CampaignTrue;
                    item.CampaignsInterest = carCampaignsDTO.CampaignsInterest;
                    item.PickUpCampaigns = carCampaignsDTO.PickUpCampaigns;
                    item.ReturnCampaigns = carCampaignsDTO.ReturnCampaigns;
                }
                await _carWriteRepository.SavaChangeAsync();
            }
            else throw new Exception("Duzgun vaxt secimi deyil!");
        }
        else throw new Exception("Duzgun Endirim Deyil!");
    }

    public async Task CarReservNextUpdate(Guid CarId, double Latitude, double Longitude)
    {
        var byCar = await _carReadRepository.GetByIdAsync(CarId);
        if (byCar is null) throw new NotFoundException("ByCar is null");

        byCar.Latitude = Latitude;
        byCar.Longitude = Longitude;
        _carWriteRepository.Update(byCar);
        await _carWriteRepository.SavaChangeAsync();
    }

    public async Task CompaignsChangePrice()
    {
        bool isComp = false;
        decimal CompanginsIntrest = 0;
        DateTime returnCompagins = DateTime.Now;

        var allCar = await _carReadRepository.GetAll().ToListAsync();
        foreach (var item in allCar)
        {
            item.isCampaigns = true;
            item.Status = CampaignsStatus.NowCampaign;
            var IsCompany = 100 - item.CampaignsInterest;
            item.CampaignsPrice = item.Price / 100;
            item.CampaignsPrice = item.CampaignsPrice * IsCompany;
            //------------
            isComp = true;
            CompanginsIntrest = (decimal)item.CampaignsInterest;
            returnCompagins = (DateTime)item.ReturnCampaigns;

        }
        await _carWriteRepository.SavaChangeAsync();
        if (isComp)
        {
            var AllUsers = _userManager.Users.ToListAsync();
            foreach (var user in AllUsers.Result)
            {
                string subject = "Campaigns";
                string html = string.Empty;
                html = html.Replace("{{CampaignsInterest}}", CompanginsIntrest.ToString());
                html = html.Replace("{{ReturnCompigns}}", returnCompagins.ToString("dddd, dd MMMM yyyy"));


                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "Campaigns.html");
                html = System.IO.File.ReadAllText(filePath);

                _emailService.Send(user.Email, subject, html);
            }
        }
    }

    public async Task CompaignsReturn()
    {
        var allCar = await _carReadRepository.GetAll().ToListAsync();
        foreach (var item in allCar)
        {
            if (item.isCampaigns == true && item.Status == CampaignsStatus.NowCampaign)
            {
                item.isCampaigns = false;
                item.Status = CampaignsStatus.ComplatedCampaign;
                item.CampaignsInterest = null;
                item.CampaignsPrice = null;
                item.PickUpCampaigns = null;
                item.ReturnCampaigns = null;
            }
        }
        await _carWriteRepository.SavaChangeAsync();
    }

    public async Task CreateAsync(CarCreateDTO carCreateDTO)
    {
        double lat = Convert.ToDouble(carCreateDTO.Longitude, CultureInfo.InvariantCulture);
        double lng = Convert.ToDouble(carCreateDTO.Longitude, CultureInfo.InvariantCulture);
        var newCar = new Car
        {
            Marka = carCreateDTO.Marka,
            Model = carCreateDTO.Model,
            Price = carCreateDTO.Price,
            Description = carCreateDTO.Description,
            Year = carCreateDTO.Year,
            Latitude = lat,
            Longitude = lng,
            PickUpCampaigns = null,
            ReturnCampaigns = null
        };


        await _carWriteRepository.AddAsync(newCar);
        await _carWriteRepository.SavaChangeAsync();


        newCar.carType = new CarType
        {
            type = carCreateDTO.CarType.type,
            CarId = newCar.Id
        };
        await _carTypeWriteRepository.AddAsync(newCar.carType);

        newCar.carCategory = new CarCategory
        {
            Category = carCreateDTO.CarCategory.Category,
            CarId = newCar.Id
        };
        await _carCategoryWriteRepository.AddAsync(newCar.carCategory);

        if (carCreateDTO.CarImages is not null)
        {
            foreach (var item in carCreateDTO.CarImages)
            {
                var carImageDto = new CarImageCreateDTO
                {
                    CarId = newCar.Id,
                    image = item
                };
                await _carImageServices.CreateAsync(carImageDto);
            }
        }

        foreach (var item in carCreateDTO.tags)
        {
            bool istag = true;
            foreach (var itemtag in await _tagReadRepository.GetAll().ToListAsync())
            {
                if (item == itemtag.tag)
                {
                    istag = false;
                    var newCarTag = new CarTag
                    {
                        CarId = newCar.Id,
                        Tag = itemtag
                    };
                    await _carTagWriteRepository.AddAsync(newCarTag);
                    await _carTagWriteRepository.SavaChangeAsync();
                    return;
                }
            }
            if (istag == true)
            {
                var newTag = new Tag
                {
                    tag = item
                };
                await _tagWriteRepository.AddAsync(newTag);
                await _tagWriteRepository.SavaChangeAsync();
            }
        }
        await _carWriteRepository.SavaChangeAsync();
    }

    public async Task<List<CarGetDTO>> GetAllAsync()
    {
        var CarAll = await _carReadRepository
            .GetAll()
            .Include(x => x.carTags)
            .Include(x => x.carType)
            .Include(x => x.carCategory)
            .Include(x => x.carImages)
            .Include(x => x.Comments)
            .Include(x => x.Reservations)
            .Include(x => x.OtherReservations)
            .Where(x => x.isReserv == false)
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync();

        if (CarAll is null) throw new NotFoundException("Car is null");
        foreach (var item in CarAll) item.Reservations = null;


        var ToDto = _mapper.Map<List<CarGetDTO>>(CarAll);
        foreach (var item in CarAll)
        {
            var toComentDto = _mapper.Map<List<CarCommentGetDTO>>(item.Comments);
            foreach (var ByToDto in ToDto)
            {
                ByToDto.CarImages = await _carImageServices.GetAllCarIdAsync(ByToDto.Id);

                if (item.Id == ByToDto.Id)
                {
                    ByToDto.carCommentGetDTO = toComentDto;
                    break;
                }
            }
        }
        return ToDto;
    }



    public async Task<List<CarGetDTO>> GetAllCarAsync()
    {
        var CarAll = await _carReadRepository
             .GetAll()
             .Include(x => x.carTags)
             .Include(x => x.carType)
             .Include(x => x.carCategory)
             .Include(x => x.carImages)
             .Include(x => x.Comments)
             .Include(x => x.OtherReservations)
             .OrderByDescending(x => x.CreatedDate)
             .ToListAsync();

        if (CarAll is null) throw new NotFoundException("Car is null");
        foreach (var item in CarAll) item.Reservations = null;


        var ToDto = _mapper.Map<List<CarGetDTO>>(CarAll);

        foreach (var item in CarAll)
        {

            var toComentDto = _mapper.Map<List<CarCommentGetDTO>>(item.Comments);
            foreach (var ByToDto in ToDto)
            {
                ByToDto.CarImages = await _carImageServices.GetAllCarIdAsync(ByToDto.Id);
                if (item.Id == ByToDto.Id)
                {
                    ByToDto.carCommentGetDTO = toComentDto;
                    break;
                }
            }
        }
        return ToDto;
    }

    public async Task<List<string>> GetAllCarMarka()
    {
        var AllCar = await _carReadRepository.GetAll().ToListAsync();
        var allMarka = new List<string>();
        foreach (var item in AllCar)
        {
            allMarka.Add(item.Marka);
        }
        return allMarka;
    }

    public async Task<List<string>> GetAllCarModel()
    {
        var AllCar = await _carReadRepository.GetAll().ToListAsync();
        var allModel = new List<string>();
        foreach (var item in AllCar)
        {
            allModel.Add(item.Model);
        }
        return allModel; throw new NotImplementedException();
    }

    public async Task<CarGetDTO> GetByIdAsync(Guid Id)
    {
        var ByCar = await _carReadRepository
            .GetAll()
            .Include(x => x.carTags)
            .Include(x => x.carType)
            .Include(x => x.carCategory)
            .Include(x => x.Comments)
            .ThenInclude(x => x.Like)
            .Include(x => x.carImages)
            .Include(x => x.Reservations)
                .OrderByDescending(x => x.CreatedDate)
            .FirstOrDefaultAsync(x => x.Id == Id);
        if (ByCar is null) throw new NotFoundException("Car is Null");
        ByCar.Reservations = null;

        var ToDto = _mapper.Map<CarGetDTO>(ByCar);
        foreach (var item in ToDto.CarImages)
        {
            ToDto.CarImages = await _carImageServices.GetAllCarIdAsync(item.CarId);
        }

        var toComentDto = _mapper.Map<List<CarCommentGetDTO>>(ByCar.Comments);
        ToDto.carCommentGetDTO = toComentDto;

        var reservCar = await _carReservationReadRepository
            .GetAll()
            .Where(x => x.CarId == ByCar.Id)
            .FirstOrDefaultAsync();

        if (reservCar is not null) ToDto.ReservationsId = reservCar.Id;
        foreach (var item in ToDto.carCommentGetDTO)
        {
            foreach (var coment in ByCar.Comments)
            {
                if (item.Id == coment.Id)
                {
                    if (coment.Like is not null)
                        item.LikeSum = coment.Like.Count;
                    else
                        item.LikeSum = 0;
                    break;
                }
            }
        }
        return ToDto;
    }

    public async Task<List<CarGetDTO>> GetByNameAsync(string? car, string? model)
    {
        var ByCar = _carReadRepository
           .GetAll()
           .Include(x => x.carTags)
           .Include(x => x.carType)
           .Include(x => x.carCategory)
           .Include(x => x.carImages)
           .Include(x => x.Comments)
           .Include(x => x.Reservations)
           .Where(x => x.isReserv == false);

        if (!string.IsNullOrEmpty(car))
        {
            ByCar = ByCar.Where(x => x.Marka.ToLower() == car.ToLower());
        }
        if (!string.IsNullOrEmpty(model))
        {
            ByCar = ByCar.Where(x => x.Model.ToLower() == model.ToLower());
        }
        var query = await ByCar.ToListAsync();

        if (query is null) throw new NotFoundException("Car is Null");
        foreach (var item in query) item.Reservations = null;

        var ToDto = _mapper.Map<List<CarGetDTO>>(query);
        foreach (var item in query)
        {

            var toComentDto = _mapper.Map<List<CarCommentGetDTO>>(item.Comments);
            foreach (var ByToDto in ToDto)
            {
                ByToDto.CarImages = await _carImageServices.GetAllCarIdAsync(ByToDto.Id);         ///ooooooooooooooooooooooooo
                if (item.Id == ByToDto.Id)
                {
                    ByToDto.carCommentGetDTO = toComentDto;
                    break;
                }
            }
        }
        return ToDto;
    }

    public async Task<int> GetCarCountAsync()
    {
        return await _carReadRepository.GetCarCountAsync();
    }

    public async Task<int> GetReservCarCountAsync()
    {
        return await _carReadRepository.GetReservCarCountAsync();
    }

    public async Task<List<CarGetDTO>> GetSearchCar(string? category,
                                                    string? type,
                                                    string? marka,
                                                    string? model,
                                                    decimal? minPrice,
                                                    decimal? maxPrice)
    {
        var ByCar = _carReadRepository
                 .GetAll()
                 .Include(x => x.carTags)
                 .Include(x => x.carType)
                 .Include(x => x.carCategory)
                 .Include(x => x.carImages)
                 .Include(x => x.Comments)
                 .Include(x => x.Reservations)
                 .Where(x => x.isReserv == false);


        if (!string.IsNullOrEmpty(category))
        {
            ByCar = ByCar.Where(x => x.carCategory.Category.ToLower() == category.ToLower());
        }
        if (!string.IsNullOrEmpty(type))
        {
            ByCar = ByCar.Where(x => x.carType.type.ToLower() == type.ToLower());
        }
        if (!string.IsNullOrEmpty(marka))
        {
            ByCar = ByCar.Where(x => x.Marka.ToLower() == marka.ToLower());
        }
        if (!string.IsNullOrEmpty(model))
        {
            ByCar = ByCar.Where(x => x.Model.ToLower() == model.ToLower());
        }
        if (minPrice is not null)
        {
            ByCar = ByCar.Where(x => x.Price >= minPrice);
        }
        if (maxPrice is not null)
        {
            ByCar = ByCar.Where(x => x.Price <= maxPrice);
        }
        var query = await ByCar.ToListAsync();

        if (query is null) throw new NotFoundException("Car is Null");
        foreach (var item in query) item.Reservations = null;

        var ToDto = _mapper.Map<List<CarGetDTO>>(query);
        foreach (var item in query)
        {
            var toComentDto = _mapper.Map<List<CarCommentGetDTO>>(item.Comments);
            foreach (var ByToDto in ToDto)
            {
                ByToDto.CarImages = await _carImageServices.GetAllCarIdAsync(ByToDto.Id);
                if (item.Id == ByToDto.Id)
                {
                    ByToDto.carCommentGetDTO = toComentDto;
                    break;
                }
            }
        }
        return ToDto;
    }

    public async Task<bool> IsCampaigns()
    {
        var allCar = await _carReadRepository.GetAll().ToListAsync();

        foreach (var item in allCar) if (item.Status == CampaignsStatus.NowCampaign) return true;
        return false;
    }

    public async Task RemoveAsync(Guid id)
    {
        var ByCar = await _carReadRepository
           .GetAll()
           .Include(x => x.carTags)
           .Include(x => x.carType)
           .Include(x => x.carCategory)
           .Include(x => x.carImages)
           .Include(x => x.Reservations)
           .FirstOrDefaultAsync(x => x.Id == id);

        if (ByCar is null) throw new NotFoundException("Car is Null");

        _carWriteRepository.Remove(ByCar);
        await _carWriteRepository.SavaChangeAsync();
    }

    public async Task ReservCarFalse(Guid Id)
    {
        var ByCar = await _carReadRepository.GetByIdAsync(Id);
        if (ByCar is null) throw new NotFoundException("Car is Null");
        Console.WriteLine("Salam");
        ByCar.isReserv = false;
        _carWriteRepository.Update(ByCar);  //False
        await _carWriteRepository.SavaChangeAsync();
    }

    public async Task ReservCarTrue(Guid Id)
    {
        var ByCar = await _carReadRepository.GetByIdAsync(Id);
        if (ByCar is null) throw new NotFoundException("Car is Null");

        ByCar.isReserv = true;
        _carWriteRepository.Update(ByCar);
        await _carWriteRepository.SavaChangeAsync();
    }

    public async Task StopCompaigns(string superAdminId)
    {
        var bySuperAdmin = await _userManager.FindByIdAsync(superAdminId);
        if (bySuperAdmin is null) throw new NotFoundException("SuperAdmin Not found");
        if (bySuperAdmin == null || !await _userManager.IsInRoleAsync(bySuperAdmin, "SuperAdmin"))
            throw new UnauthorizedAccessException("You do not have permission to perform this action.");

        if (bySuperAdmin is not null)
        {
            var allCar = await _carReadRepository.GetAll().ToListAsync();
            foreach (var item in allCar)
            {
                item.isCampaigns = false;
                item.Status = CampaignsStatus.ComplatedCampaign;
                item.CampaignsInterest = null;
                item.CampaignsPrice = null;
                item.PickUpCampaigns = null;
                item.ReturnCampaigns = null;
            }
            await _carWriteRepository.SavaChangeAsync();
        }
        else throw new NotFoundException("SuperAdmin not found");
    }

    public async Task UpdateAsync(Guid id, CarUpdateDTO carUpdateDTO)
    {
        var ByCar = await _carReadRepository
           .GetAll()                    //2bab2a3b-6435-48c5-f86d-08db9f6de8f1
           .Include(x => x.carTags)
           .Include(x => x.carType)
           .Include(x => x.carCategory)
           .Include(x => x.carImages)
           .Include(x => x.Reservations)
           .FirstOrDefaultAsync(x => x.Id == id);
        if (ByCar is null) throw new NotFoundException("Car is Null");

        ByCar.Marka = carUpdateDTO.Marka;
        ByCar.Model = carUpdateDTO.Model;
        ByCar.Price = carUpdateDTO.Price;
        ByCar.Year = carUpdateDTO.Year;
        ByCar.Description = carUpdateDTO.Description;
        ByCar.isReserv = carUpdateDTO.isReserv;


        double lat = Convert.ToDouble(carUpdateDTO.Longitude, CultureInfo.InvariantCulture);
        double lng = Convert.ToDouble(carUpdateDTO.Longitude, CultureInfo.InvariantCulture);

        ByCar.Latitude = lat;
        ByCar.Longitude = lng;
        if (ByCar.isCampaigns==true)
        {
            var interest = 100 - ByCar.CampaignsInterest;
            ByCar.CampaignsPrice = (ByCar.Price * (decimal)interest) / 100;
        }
        if (ByCar.carType is not null)
        {
            var carUpdateType = new CarTypeUpdateDTO
            {
                Type = carUpdateDTO.CarType.Type
            };
            var CarTypeID = ByCar.carType.Id;
            await _carTypeService.UpdateAsync(CarTypeID, carUpdateType);
        }
        else
        {
            ByCar.carType = new CarType
            {
                type = carUpdateDTO.CarType.Type,
                CarId = ByCar.Id
            };
            await _carTypeWriteRepository.AddAsync(ByCar.carType);
        }

        if (ByCar.carCategory is not null)
        {
            var carUpdateCategory = new CarCategory
            {
                CarId = ByCar.Id,
                Category = carUpdateDTO.CarCategory.category
            };

            _carCategoryWriteRepository.Update(carUpdateCategory);
        }
        else
        {
            ByCar.carCategory = new CarCategory
            {
                Category = carUpdateDTO.CarCategory.category,
                CarId = ByCar.Id
            };
            await _carCategoryWriteRepository.AddAsync(ByCar.carCategory);
        }

        foreach (var item in carUpdateDTO.tags)
        {
            var newTag = new Tag  { tag = item };
            await _tagWriteRepository.AddAsync(newTag);
            await _tagWriteRepository.SavaChangeAsync();
            foreach (var tag in await _carTagReadRepository.GetAll().Where(x => x.CarId == ByCar.Id).ToListAsync())
            {
                tag.TagId = newTag.Id;
                _carTagWriteRepository.Update(tag);
            }
        }

        if (carUpdateDTO.CarImages is not null)
        {
            foreach (var item in carUpdateDTO.CarImages)
            {
                if (ByCar.carImages is null)
                {
                    var carImageCreateDto = new CarImageCreateDTO
                    {
                        CarId = ByCar.Id,
                        image = item
                    };
                    await _carImageServices.CreateAsync(carImageCreateDto);
                }
                else
                {
                    var carImageDto = new CarImageUpdateDTO
                    {
                        CarId = ByCar.Id,
                        image = item
                    };
                    foreach (var updateImage in ByCar.carImages)
                    {
                        await _carImageServices.UpdateAsync(updateImage.Id, carImageDto);
                    }
                }
            }
        }

        _carWriteRepository.Update(ByCar);
        await _carWriteRepository.SavaChangeAsync();
    }
}
