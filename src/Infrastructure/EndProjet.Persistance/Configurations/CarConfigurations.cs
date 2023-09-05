using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Configurations;

public class CarConfigurations : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.Property(x => x.Marka).IsRequired().HasMaxLength(130);
        builder.Property(x => x.Model).IsRequired().HasMaxLength(120);
        builder.Property(x => x.Price).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.Year).IsRequired().HasMaxLength(1120);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(12000);
    }
}
