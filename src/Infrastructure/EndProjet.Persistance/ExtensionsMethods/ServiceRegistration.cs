﻿using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.Abstraction.Services.Game;
using EndProject.Application.Validators.SliderValidators;
using EndProject.Domain.Entitys.Identity;
using EndProjet.Persistance.Context;
using EndProjet.Persistance.Implementations.Repositories.EntityRepository;
using EndProjet.Persistance.Implementations.Services;
using EndProjet.Persistance.MapperProfiles;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace EndProjet.Persistance.ExtensionsMethods;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(services.BuildServiceProvider().GetService<IConfiguration>().GetConnectionString("Default"));
        });

        //services.AddAuthentication()
        //       .AddGoogle(options =>
        //       {
        //           options.ClientId = "http://91997614652-1q2taif2sptoou1dahqsiripc4u5e0b6.apps.googleusercontent.com";
        //           options.ClientSecret = "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";
        //       });


        //Repository
        services.AddScoped<ISliderReadRepository, SliderReadRepository>();
        services.AddScoped<ISliderWriteRepository, SliderWriteRepository>();
        services.AddScoped<ICarTypeWriteRepository, CarTypeWriteRepository>();
        services.AddScoped<ICarTypeReadRepository, CarTypeReadRepository>();
        services.AddScoped<ICarReadRepository, CarReadRepository>();
        services.AddScoped<ICarWriteRepository, CarWriteRepository>();
        services.AddScoped<ICarImageReadRepository, CarImageReadRepository>();
        services.AddScoped<ICarImageWriteRepository, CarImageWriteRepository>();
        services.AddScoped<ICarCategoryReadRepository, CarCategoryReadRepository>();
        services.AddScoped<ICarCategoryWriteRepository, CarCategoryWriteRepository>();
        services.AddScoped<ITagReadRepository, TagReadRepository>();
        services.AddScoped<ITagWriteRepository, TagWriteRepository>();
        services.AddScoped<ICarTagReadRepository, CarTagReadRepository>();
        services.AddScoped<ICarTagWriteRepository, CarTagWriteRepository>();
        services.AddScoped<ICarCommentReadRepository, CarCommentReadRepository>();
        services.AddScoped<ICarCommentWriteRepository, CarCommentWriteRepository>();
        services.AddScoped<ICarReservationReadRepository, CarReservationReadRepository>();
        services.AddScoped<ICarReservationWriteRepository, CarReservationWriteRepository>();
        services.AddScoped<IPickupLocationReadRepository, PickupLocationReadRepository>();
        services.AddScoped<IPickupLocationWriteRepository, PickupLocationWriteRepository>();
        services.AddScoped<IReturnLocationReadRepository, ReturnLocationReadRepository>();
        services.AddScoped<IReturnLocationWriteRepository, ReturnLocationWriteRepository>();
        services.AddScoped<IChauffeursReadRepository, ChauffeursReadRepository>();
        services.AddScoped<IChauffeursWriteRepository, ChauffeursWriteRepository>();
        services.AddScoped<IOtherCarReservationReadRepository, OtherCarReservationReadRepository>();
        services.AddScoped<IOtherCarReservationWriteRepository, OtherCarReservationWriteRepository>();
        services.AddScoped<IAdvantageReadRepository, AdvantageReadRepository>();
        services.AddScoped<IAdvantageWriteRepository, AdvantageWriteRepository>();
        services.AddScoped<IFaqReadRepository, FaqReadRepository>();
        services.AddScoped<IFaqWriteRepository, FaqWriteRepository>();
        services.AddScoped<IBlogReadRepository, BlogReadRepository>();
        services.AddScoped<IBlogWriteRepository, BlogWriteRepository>();
        services.AddScoped<IBlogImageReadRepository, BlogImageReadRepository>();
        services.AddScoped<IBlogImageWriteRepository, BlogImageWriteRepository>();
        services.AddScoped<IBasketReadRepository, BasketReadRepository>();
        services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
        services.AddScoped<IBasketProductReadRepository, BasketProductReadRepository>();
        services.AddScoped<IBasketProductWriteRepository, BasketProductWriteRepository>();
        services.AddScoped<ILikeReadRepository, LikeReadRepository>();
        services.AddScoped<ILikeWriteRepository, LikeWriteRepository>();
        services.AddScoped<IUserMessageWriteRepository, UserMessageWriteRepository>();
        services.AddScoped<IUserMessageReadRepository, UserMessageReadRepository>();
        services.AddScoped<ICommunicationReadRepository, CommunicationReadRepository>();
        services.AddScoped<ICommunicationWriteRepository, CommunicationWriteRepository>();
        services.AddScoped<ICampaignStatistikaReadRepository, CampaignStatistikaReadRepository>();
        services.AddScoped<ICampaignStatistikaWriteRepository, CampaignStatistikaWriteRepository>();
        services.AddScoped<IWishlistReadRepository, WishlistReadRepository>();
        services.AddScoped<IWishlistWriteRepository, WishlistWriteRepository>();
        services.AddScoped<IWishlistProductReadRepository, WishlistProductReadRepository>();
        services.AddScoped<IWishlistProductWriteRepository, WishlistProductWriteRepository>();
        services.AddScoped<IGameCarWriteRepository, GameCarWriteRepository>();
        services.AddScoped<IGameCarReadRepository, GameCarReadRepository>();
        services.AddScoped<ITripeReadRepository, TripeReadRepository>();
        services.AddScoped<ITripeWriteRepository, TripeWriteRepository>();
        services.AddScoped<ITripNoteReadRepository, TripNoteReadRepository>();
        services.AddScoped<ITripNoteWriteRepository, TripNoteWriteRepository>();
        services.AddScoped<IShareTripReadRepository, ShareTripReadRepository>();
        services.AddScoped<IShareTripWriteRepository, ShareTripWriteRepository>();


        //Service
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISliderService, SliderService>();
        services.AddScoped<ICarTypeService, CarTypeService>();
        services.AddScoped<ICarServices, CarServices>();
        services.AddScoped<ICarImageServices, CarImageServices>();
        services.AddScoped<ICarCategoryServices, CarCategoryServices>();
        services.AddScoped<ICarCommentServices, CarCommentServices>();
        services.AddScoped<ICarReservationServices, CarReservationServices>();
        services.AddScoped<IChauffeursServices, ChauffeursServices>();
        services.AddScoped<IOtherCarReservationServices, OtherCarReservationServices>();
        services.AddScoped<IAdvantageServices, AdvantageServices>();
        services.AddScoped<IFaqServices, FaqServices>();
        services.AddScoped<IBlogImageServices, BlogImageServices>();
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<ILikeServices, LikeServices>();
        services.AddScoped<IBasketServices, BasketServices>();
        services.AddScoped<IBasketProducServices, BasketProducServices>();
        services.AddScoped<IWishlistServices, WishlistServices>();
        services.AddScoped<IWishlistProductServices, WishlistProductServices>();
        services.AddScoped<ISendUserMessageServices, SendUserMessageServices>();
        services.AddScoped<ICommunicationServices, CommunicationServices>();
        services.AddScoped<IPickUpServices, PickUpServices>();
        services.AddScoped<ICampaignStatistikaServices, CampaignStatistikaServices>();
        services.AddScoped<IGameCarServices, GameCarServices>();
        services.AddScoped<ITripServices, TripServices>();
        services.AddScoped<ITripNoteServices, TripNoteServices>();
        services.AddScoped<IShareTripServices, ShareTripServices>();




        //User
        services.AddIdentity<AppUser, IdentityRole>(Options =>
        {
            Options.User.RequireUniqueEmail = true;
            Options.Password.RequireNonAlphanumeric = true;
            Options.Password.RequiredLength = 8;
            Options.Password.RequireDigit = true;
            Options.Password.RequireUppercase = true;
            Options.Password.RequireLowercase = true;

            Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            Options.Lockout.MaxFailedAccessAttempts = 3;
            Options.Lockout.AllowedForNewUsers = true;
        }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();


        //Mapper
        services.AddAutoMapper(typeof(SliderProfile).Assembly);


        //Validator
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<SliderGetDtoValidator>();
    }
}
