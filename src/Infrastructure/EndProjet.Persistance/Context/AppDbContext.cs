using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Context;

public class AppDbContext:IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Group_User>()
        .HasKey(us => new { us.GroupId, us.AppUserId });

        modelBuilder.Entity<Group_User>()
            .HasOne(us => us.Group)
            .WithMany(u => u.group_Users)
            .HasForeignKey(us => us.GroupId);

        modelBuilder.Entity<Group_User>()
            .HasOne(us => us.AppUser)
            .WithMany(s => s.group_Users)
            .HasForeignKey(us => us.AppUserId);


        modelBuilder.Entity<PostLike>()
            .HasOne(pl => pl.Posts)
            .WithMany(p => p.postLikes)
            .HasForeignKey(pl => pl.PostsId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Like>()
           .HasOne(l => l.AppUser)
           .WithMany()
           .OnDelete(DeleteBehavior.Restrict);


        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Comments> Comments { get; set; }   
    public DbSet<Group> Groups { get; set; }
    public DbSet<Group_User> Group_Users { get; set; }
    public DbSet<GroupMessage> GroupMessages { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<PostLike> PostsLikes { get; set; }
    public DbSet<Posts> Posts { get; set; }
    public DbSet<Tags> Tags { get; set; }
    public DbSet<Post_Tag> Post_Tags { get; set; }
}
