using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Comments;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Identity;
using EndProjet.Persistance.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class CommentService : ICommentService
{
    private readonly ICommentReadRepository _commentReadRepository;
    private readonly ICommentWriteRepository _commentWriteRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public CommentService(ICommentReadRepository commentReadRepository,
                          ICommentWriteRepository commentWriteRepository,
                          IHttpContextAccessor contextAccessor,
                          UserManager<AppUser> userManager,
                          IMapper mapper)
    {
        _commentReadRepository = commentReadRepository;
        _commentWriteRepository = commentWriteRepository;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task AddAsync(CommentCreateDTO commentCreateDTO)
    {
        Comments newComments = new()
        {
            message = commentCreateDTO.Comment,
            AppUserId = commentCreateDTO.AppUserId,
            PostsId = commentCreateDTO.PostId
        };
        await _commentWriteRepository.AddAsync(newComments);
        await _commentWriteRepository.SavaChangeAsync();
    }

    public async Task<CommentGetDTO> GetByIdAsync(Guid Id)
    {
        var ByComment = await _commentReadRepository.GetByIdAsync(Id);
        var EntityToDto = _mapper.Map<CommentGetDTO>(ByComment);
        EntityToDto.PostId = (Guid)ByComment.PostsId;
        EntityToDto.Comment = ByComment.message;
        return EntityToDto;
    }

    public async Task<List<CommentGetDTO>> GettAllAsync()
    {
        var comments = await _commentReadRepository.GetAll().ToListAsync();
        var allComents = new List<CommentGetDTO>();
        foreach (var item in comments)
        {
            var comment = new CommentGetDTO
            {
                Comment = item.message,
                AppUserId = item.AppUserId,
                PostId = (Guid)item.PostsId
            };
            allComents.Add(comment);
        }
        return allComents;
    }

    public async Task RemoveAsync(Guid Id)
    {
        var ByComment = await _commentReadRepository.GetByIdAsync(Id);
        if (ByComment is null) throw new NotFoundException("Comment is Null");

        _commentWriteRepository.Remove(ByComment);
        await _commentWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid Id, CommentUpdateDTO commentUpdateDTO)
    {
        var ByComment = await _commentReadRepository.GetByIdAsync(Id);
        if (ByComment is null) throw new NotFoundException("Comment is Null");

        ByComment.message = commentUpdateDTO.Comment;
        ByComment.AppUserId = commentUpdateDTO.AppUserId;
        ByComment.PostsId = commentUpdateDTO.PostId;

        _commentWriteRepository.Update(ByComment);
        await _commentWriteRepository.SavaChangeAsync();
    }

    private string GetUserId()
    {
        //var user = _contextAccessor.HttpContext.User;
        string userId = "8a244e85-22bd-42a2-aa8e-0d5f93d9bdb4";
        return userId;
    }
}
