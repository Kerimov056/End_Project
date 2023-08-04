using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndProjet.Persistance.Configurations;

public class GroupConficurations : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.Property(x => x.GroupName).IsRequired().HasMaxLength(120);
        builder.Property(x => x.ImagePath).HasMaxLength(900);
    }
}
