﻿using EndProject.Application.DTOs.CarReservation;
using EndProject.Application.DTOs.Slider;

namespace EndProject.Application.Abstraction.Services;

public interface ICarReservationServices
{
    Task<List<CarReservationGetDTO>> GetAllAsync();
    Task CreateAsync(CarReservationCreateDTO carReservationCreateDTO);
    Task<CarReservationGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, CarReservationUpdateDTO carReservationUpdateDTO);
    Task RemoveAsync(Guid id);
}
