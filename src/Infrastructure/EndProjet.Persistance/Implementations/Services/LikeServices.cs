﻿using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Like;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class LikeServices : ILikeServices
{
    private readonly ILikeReadRepository _likeReadRepository;
    private readonly ILikeWriteRepository _likeWriteRepository;
    private readonly ICarCommentWriteRepository _carCommentWriteRepository;
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public LikeServices(ILikeReadRepository likeReadRepository,
                        ILikeWriteRepository likeWriteRepository,
                        IMapper mapper,
                        ICarCommentWriteRepository carCommentWriteRepository,
                        AppDbContext appDbContext)
    {
        _likeReadRepository = likeReadRepository;
        _likeWriteRepository = likeWriteRepository;
        _carCommentWriteRepository = carCommentWriteRepository;
        _mapper = mapper;
        _appDbContext = appDbContext;
    }

    public async Task CreateAsync(string UserId, Guid CarCommentId)
    {
        var byLike = await _likeReadRepository
            .GetAll()
            .Where(x => x.AppUserId == UserId)
            .Where(x => x.CarCommentId == CarCommentId)
            .FirstOrDefaultAsync();

        var byComment = await _appDbContext.CarComments.FindAsync(CarCommentId);

        if (byLike is null)
        {
            var newLike = new Like
            {
                AppUserId = UserId,
                CarCommentId = CarCommentId,
            };
            await _likeWriteRepository.AddAsync(newLike);
        }
        else
        {
            _likeWriteRepository.Remove(byLike);
        }

        await _carCommentWriteRepository.SavaChangeAsync();
        await _likeWriteRepository.SavaChangeAsync();
    }

    public async Task<List<LikeGetDTO>> GetAllAsync()
    {
        var likes = await _likeReadRepository.GetAll().ToListAsync();
        return _mapper.Map<List<LikeGetDTO>>(likes);
    }

    public async Task<LikeGetDTO> GetByIdAsync(Guid Id)
    {
        var like = await _likeReadRepository.GetByIdAsync(Id);
        return _mapper.Map<LikeGetDTO>(like);

    }

    public async Task RemoveAsync(Guid id)
    {
        var Bylike = await _likeReadRepository.GetByIdAsync(id);
        _likeWriteRepository.Remove(Bylike);
        await _likeWriteRepository.SavaChangeAsync();
    }

}
