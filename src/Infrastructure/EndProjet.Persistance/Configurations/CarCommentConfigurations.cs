using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Configurations;

internal class CarCommentConfigurations : IEntityTypeConfiguration<CarComment>
{
    public void Configure(EntityTypeBuilder<CarComment> builder)
    {
        builder.Property(x => x.Comment).IsRequired().HasMaxLength(1000);
    }
}
