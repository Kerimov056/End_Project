using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Identity;
using EndProjet.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class LikeService : ILikeService
{
    private readonly AppDbContext _appDbContext;

    public LikeService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task LikeCommentAsync(string userId, Guid commentId)
    {
        var comment = await _appDbContext.Comments
            .FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment == null)
        {
            return;
        }

        var existingLike = await _appDbContext.Likes
            .FirstOrDefaultAsync(l => l.CommentsId == commentId && l.AppUserId == userId);
        if (existingLike == null)
        {
            var newLike = new Like
            {
                CommentsId = commentId,
                AppUserId = userId,
                likeSum = 1
            };
            await _appDbContext.Likes.AddAsync(newLike);
        }
        else
        {
            existingLike.likeSum++;
        }

        await _appDbContext.SaveChangesAsync();
    }
    public async Task<int> GetLikeCountForComment(Guid commentId)
    {
        var comment = await _appDbContext.Comments
            .Include(c => c.Likes)
            .FirstOrDefaultAsync(c => c.Id == commentId);

        if (comment == null)
        {
            return 0;
        }
        return comment.Likes.Count;
    }



    public async Task UnlikeCommentAsync(string userId, Guid commentId)
    {
        var comment = await _appDbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment == null)
        {
            return;
        }

        var existingLike = await _appDbContext.Likes.FirstOrDefaultAsync(l => l.CommentsId == commentId && l.AppUserId == userId);
        if (existingLike != null)
        {
            if (existingLike.likeSum > 0)
            {
                existingLike.likeSum--;
            }
            _appDbContext.Likes.Remove(existingLike);
        }

        await _appDbContext.SaveChangesAsync();
    }
}