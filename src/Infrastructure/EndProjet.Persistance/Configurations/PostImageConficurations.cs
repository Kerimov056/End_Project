using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndProjet.Persistance.Configurations;

public class PostImageConficurations : IEntityTypeConfiguration<PostImage>
{
    public void Configure(EntityTypeBuilder<PostImage> builder)
    {
        builder.Property(x => x.imagePath).HasMaxLength(756);
    }
}
