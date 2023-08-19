﻿using EndProject.Application.DTOs.Faq;
using EndProject.Application.DTOs.Slider;

namespace EndProject.Application.Abstraction.Services;

public interface IFaqServices
{
    Task<List<FaqGetDTO>> GetAllAsync();
    Task CreateAsync(FaqCreateDTO faqCreateDTO);
    Task<FaqGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, FaqUpdateDTO faqUpdateDTO);
    Task RemoveAsync(Guid id);
}
