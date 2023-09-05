using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Configurations;

public class CarImageConfigurations : IEntityTypeConfiguration<CarImage>
{
    public void Configure(EntityTypeBuilder<CarImage> builder)
    {
        builder.Property(x => x.imagePath).IsRequired().HasMaxLength(12200);
    }
}