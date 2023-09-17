using EndProject.Application.DTOs.Car;
using EndProject.Application.DTOs.CarType;

namespace EndProject.Application.Abstraction.Services;

public interface ICarServices
{
    Task<List<CarGetDTO>> GetAllCarAsync();
    Task<List<CarGetDTO>> GetAllAsync();
    Task CreateAsync(CarCreateDTO carCreateDTO);
    Task<CarGetDTO> GetByIdAsync(Guid Id);
    Task<CarGetDTO> GetByIdIsAsync(Guid Id);
    Task<byte[]> GetByIdQrCode(Guid Id);
    Task<List<string>> GetAllCarMarka();
    Task<List<string>> GetAllCarModel();
    Task<List<CarGetDTO>> GetByNameAsync(string? car, string? model);
    Task<List<CarGetDTO>> GetSearchCar(string? category,
                                       string? type,
                                       string? marka,
                                       string? model,
                                       decimal? minPrice,
                                       decimal? maxPrice);
    Task UpdateAsync(Guid id, CarUpdateDTO carUpdateDTO);
    Task ReservCarTrue(Guid Id);
    Task ReservCarFalse(Guid Id);
    Task RemoveAsync(Guid id);
    Task<int> GetCarCountAsync();
    Task<int> GetReservCarCountAsync();

    //--------------------------------
                //Compagins

    Task Campaigns(CarCampaignsDTO carCampaignsDTO);
    Task CompaignsChangePrice();
    Task CompaignsReturn();
    Task<bool> IsCampaigns();
    Task StopCompaigns(string superAdminId);
    Task<List<CarGetDTO>> GetAllCompaignAsync();



    //---------------------------------
    // Car reservation sonrasi olan update
    Task CarReservNextUpdate(Guid CarId, double Latitude, double Longitude);
}
