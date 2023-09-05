using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Configurations;

public class TagConficurations : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(x => x.tag).IsRequired().HasMaxLength(140);
    }
}
