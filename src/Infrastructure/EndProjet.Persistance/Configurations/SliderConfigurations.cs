using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndProjet.Persistance.Configurations;

public class SliderConfigurations : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {
        builder.Property(x => x.Imagepath).IsRequired().HasMaxLength(12000);
    }
}
