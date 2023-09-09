using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Configurations;

public class ChauffeursConfigurations : IEntityTypeConfiguration<Chauffeurs>
{
    public void Configure(EntityTypeBuilder<Chauffeurs> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Number).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Price).IsRequired().HasMaxLength(5000);
        builder.Property(x => x.imagePath).IsRequired().HasMaxLength(55000);
    }
}
