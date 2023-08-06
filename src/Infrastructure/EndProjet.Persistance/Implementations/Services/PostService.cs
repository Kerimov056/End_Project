using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Comments;
using EndProject.Application.DTOs.NewTag;
using EndProject.Application.DTOs.Post;
using EndProject.Application.DTOs.Post_Tag;
using EndProject.Application.DTOs.Tag;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Identity;
using EndProjet.Persistance.Context;
using EndProjet.Persistance.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class PostService : IPostService
{
    private readonly IPostReadRepository _postReadRepository;
    private readonly IPostWriteRepository _postWriteRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<AppUser> _userManager;
    private readonly INewTagService _newTagService;
    private readonly IPostImageService _postImageService;
    private readonly ITagService _tagService;
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public PostService(IPostReadRepository postReadRepository,
                       IPostWriteRepository postWriteRepository,
                       IHttpContextAccessor httpContextAccessor,
                       IPostImageService postImageService,
                       ITagService tagService,
                       INewTagService newTagService,
                       AppDbContext appDbContext,
                       UserManager<AppUser> userManager,
                       IMapper mapper)
    {
        _postReadRepository = postReadRepository;
        _postWriteRepository = postWriteRepository;
        _contextAccessor = httpContextAccessor;
        _postImageService = postImageService;
        _tagService = tagService;
        _newTagService= newTagService;
        _appDbContext = appDbContext;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task AddAsync(PostCreateDTO postCreateDTO)
    {
        var ByUser = GetUserId();
        var NewPost = _mapper.Map<Posts>(postCreateDTO);
        NewPost.AppUserId = ByUser;

        await _postWriteRepository.AddAsync(NewPost); 
        await _postWriteRepository.SavaChangeAsync();

        foreach (var imageDto in postCreateDTO.Images)
        {
            var newPostImage = _mapper.Map<PostImageCreateDTO>(imageDto);
            await _postImageService.AddAsync(NewPost.Id, newPostImage);
        }

        //foreach (var tag in postCreateDTO.NewTagCreateDTOs)
        //{
        //    TagCreateDTO tagCreateDTO = new()
        //    {
        //        Tag = tag.Tag
        //    };
        //    await _tagService.AddAsync(tagCreateDTO);
        //}


        var tags = new List<NewTagCreateDTO>();
        foreach (var item in tags)
        {
            var tagCreateDto = new TagCreateDTO
            {
                Tag = item.Tag
            };
            await _tagService.AddAsync(tagCreateDto);
        }
        
        foreach (var item in await _tagService.GettAllAsync())
        {
            var Post_Tag = new Post_Tag
            {
                TagsId = item.Id,
                PostsId = NewPost.Id
            };
            await _appDbContext.Post_Tags.AddAsync(Post_Tag);
        }
        await _postWriteRepository.SavaChangeAsync();
    }

    public async Task<PostGetDTO> GetByIdAsync(Guid Id)
    {
        var Posts = await _postReadRepository
          .GetAll()
          .Include(x => x.postImage)
          .Include(x => x.comments)
          .Include(x => x.postLikes)
          .Include(x => x.Post_Tags)
          .ThenInclude(x => x.Tags)
          .FirstOrDefaultAsync(x => x.Id==Id);

        if (Posts is null) throw new NotFoundException("Post Is Null");

        var EntityToDto = _mapper.Map<PostGetDTO>(Posts);
        return EntityToDto;
    }

    public async Task<List<PostGetDTO>> GettAllAsync()
    {
        var Posts = await _postReadRepository
           .GetAll()
           .Include(x => x.postImage)
           .Include(x => x.comments)
           .Include(x => x.postLikes)
           .Include(x => x.Post_Tags)
           .ThenInclude(x => x.Tags)
           .ToListAsync();
        var EntityToDto = _mapper.Map<List<PostGetDTO>>(Posts);

        //var EntityToDto = new List<PostGetDTO>();

        //foreach (var item in Posts)
        //{
        //    var psotDto = new PostGetDTO
        //    {
        //        Id = item.Id,
        //        Message = item.message,
        //        AppUserId = item.AppUserId,
        //        Images = new List<PostImageGetDTO>(),
        //        commentGetDTOs = new List<CommentGetDTO>(),
        //        postLikeGetDTOs= new List<PostLikeGetDTO>(),
        //        Post_TagGetDTO = new List<Post_TagGetDTO>()
        //    };


        //    foreach (var image in item.postImage)
        //    {
        //        var postImageGetDTO = new PostImageGetDTO
        //        {
        //            ImagePath = image.imagePath,
        //        };
        //    }

        //    foreach (var coment in item.comments)
        //    {
        //        var CommentGetDTO = new CommentGetDTO
        //        {
        //            AppUserId= item.AppUserId,
        //            PostId = item.Id,
        //            Comment = coment.message,
        //        };
        //    }

        //    foreach (var postLike in item.postLikes)
        //    {
        //        var postLikeGetDTO = new PostLikeGetDTO
        //        {
        //            likeSum = postLike.likeSum,
        //            PostId = postLike.PostsId,
        //            AppUserId= postLike.AppUserId
        //        };
        //    }

        //    foreach (var Postag in item.Post_Tags)
        //    {
        //        var postTags = new Post_Tag
        //        {
        //            PostsId = Postag.PostsId,
        //            TagsId = Postag.TagsId
        //        };
        //    }

        //    EntityToDto.Add(psotDto);
        //}

        return EntityToDto;
    }

    public async Task RemoveAsync(Guid Id)
    {
        var Posts = await _postReadRepository
        .GetAll()
        .Include(x => x.postImage)
        .Include(x => x.comments)
        .Include(x => x.postLikes)
        .Include(x => x.Post_Tags)
        .ThenInclude(x => x.Tags)
        .FirstOrDefaultAsync(x => x.Id == Id);

        if (Posts is null) throw new NotFoundException("Post Is Null");

        _postWriteRepository.Remove(Posts);
        await _postWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid Id, PostCreateDTO postCreateDTO)
    {
        var Posts = await _postReadRepository
        .GetAll()
        .Include(x => x.postImage)
        .Include(x => x.comments)
        .Include(x => x.postLikes)
        .Include(x => x.Post_Tags)
        .ThenInclude(x => x.Tags)
        .FirstOrDefaultAsync(x => x.Id == Id);

        if (Posts is null) throw new NotFoundException("Post Is Null");

        _mapper.Map(postCreateDTO, Posts);
        _postWriteRepository.Update(Posts);
        await _postWriteRepository.SavaChangeAsync();
    }


    private string GetUserId()
    {
        //var user = _contextAccessor.HttpContext.User;
        string userId = "8a244e85-22bd-42a2-aa8e-0d5f93d9bdb4";
        return userId;
    }
}




