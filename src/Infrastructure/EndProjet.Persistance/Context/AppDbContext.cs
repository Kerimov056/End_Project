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
        //modelBuilder.Entity<TypeCategory>()
        //    .HasOne(x => x.type)
        //    .WithMany(d => d.typeCategories)
        //    .HasForeignKey(x => x.typeId)
        //    .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<TypeCategory>()
        //    .HasOne(f => f.category)
        //    .WithMany(s => s.typeCategories)
        //    .HasForeignKey(us => us.categoryId)
        //    .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<CarTag>()
            .HasOne(x => x.Car)
            .WithMany(d => d.carTags)
            .HasForeignKey(x => x.CarId);

        modelBuilder.Entity<CarTag>()
            .HasOne(f => f.Tag)
            .WithMany(s => s.carTags)
            .HasForeignKey(us => us.TagId);


        modelBuilder.Entity<CarReservation>()
                 .HasOne(cr => cr.PickupLocation)
                 .WithMany()
                 .HasForeignKey(cr => cr.PickupLocationId)
                 .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CarReservation>()
            .HasOne(cr => cr.ReturnLocation)
            .WithMany()
            .HasForeignKey(cr => cr.ReturnLocationId)
            .OnDelete(DeleteBehavior.Restrict);

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
    public DbSet<Location> Locations { get; set; }
    public DbSet<Chauffeurs> Chauffeurs { get; set; }
}

