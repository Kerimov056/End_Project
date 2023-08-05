using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndProjet.Persistance.Configurations;

public class NewTagConficurations : IEntityTypeConfiguration<NewTag>
{
    public void Configure(EntityTypeBuilder<NewTag> builder)
    {
        builder.Property(x => x.Tag).HasMaxLength(120);
    }
}
