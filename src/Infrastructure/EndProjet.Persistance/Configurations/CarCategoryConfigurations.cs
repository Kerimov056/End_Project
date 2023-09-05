using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Configurations;

public class CarCategoryConfigurations : IEntityTypeConfiguration<CarCategory>
{
    public void Configure(EntityTypeBuilder<CarCategory> builder)
    {
        builder.Property(x => x.Category).IsRequired().HasMaxLength(100);
    }
}
