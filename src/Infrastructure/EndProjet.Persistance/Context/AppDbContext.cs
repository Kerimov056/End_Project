using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Context;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Like>()
            .HasOne(l => l.CarComment)
            .WithMany(c => c.Like)
            .HasForeignKey(l => l.CarCommentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CarTag>()
            .HasOne(x => x.Car)
            .WithMany(d => d.carTags)
            .HasForeignKey(x => x.CarId);

        modelBuilder.Entity<CarTag>()
            .HasOne(f => f.Tag)
            .WithMany(s => s.carTags)
            .HasForeignKey(us => us.TagId);


        base.OnModelCreating(modelBuilder);

    }

    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarType> CarTypes { get; set; }
    public DbSet<CarCategory> CarCategories { get; set; }
    //public DbSet<TypeCategory> TypeCategories { get; set; }
    public DbSet<CarImage> CarImages { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<CarTag> CarTags { get; set; }
    public DbSet<CarReservation> CarReservations { get; set; }
    public DbSet<OtherCarReservation> OtherCarReservations { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Chauffeurs> Chauffeurs { get; set; }
    public DbSet<CarComment> CarComments { get; set; }
    public DbSet<PickupLocation> PickupLocations { get; set; }
    public DbSet<ReturnLocation> ReturnLocations { get; set; }
    public DbSet<Advantage> Advantages { get; set; }
    public DbSet<Faq> Faqs { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<BlogImage> BlogImages { get; set; }
    public DbSet<BasketProduct> BasketProducts { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<SendUserMessage> SendUserMessages { get; set; }
    public DbSet<Communication> Communications { get; set; }
    //public DbSet<CommentLike> CommentLikes { get; set; }
}

