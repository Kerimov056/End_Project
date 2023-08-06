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
        var comment = await _appDbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment == null)
        {
            // Comment bulunamadı, hata ver veya işlemi sonlandır.
            return;
        }

        var existingLike = await _appDbContext.Likes.FirstOrDefaultAsync(l => l.CommentsId == commentId && l.AppUserId == userId);
        if (existingLike == null)
        {
            // Daha önce like yapılmamış, yeni bir Like oluştur ve beğeni sayısını 1 olarak ayarla.
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
            // Daha önce like yapılmış, beğeni sayısını artır.
            existingLike.likeSum++;
        }

        await _appDbContext.SaveChangesAsync();
    }

    public async Task<int> GetLikeCountForComment(Guid commentId)
    {
        var comment = await _appDbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment == null)
        {
            // Comment bulunamadı, hata ver veya işlemi sonlandır.
            return 0;
        }

        // Yorumun beğeni sayısını döndür.
        return comment.Likes.Count;
    }

    public async Task<List<AppUser>> GetUsersWhoLikedComment(Guid commentId)
    {
        var comment = await _appDbContext.Comments
            .Include(c => c.Likes)
            .ThenInclude(l => l.AppUser)
            .FirstOrDefaultAsync(c => c.Id == commentId);

        if (comment == null)
        {
            // Comment bulunamadı, hata ver veya işlemi sonlandır.
            return new List<AppUser>();
        }

        // Yorumu beğenen kullanıcıların listesini döndür.
        return comment.Likes.Select(like => like.AppUser).ToList();
    }

    public async Task UnlikeCommentAsync(string userId, Guid commentId)
    {
        var comment = await _appDbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment == null)
        {
            // Comment bulunamadı, hata ver veya işlemi sonlandır.
            return;
        }

        var existingLike = await _appDbContext.Likes.FirstOrDefaultAsync(l => l.CommentsId == commentId && l.AppUserId == userId);
        if (existingLike != null)
        {
            // Beğeni var, beğeni sayısını düşür ve beğeniyi sil.
            if (existingLike.likeSum > 0)
            {
                existingLike.likeSum--;
            }
            _appDbContext.Likes.Remove(existingLike);
        }

        await _appDbContext.SaveChangesAsync();
    }
}