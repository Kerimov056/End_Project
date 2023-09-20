using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndProjet.Persistance.Configurations;

public class GameCarConfigurations : IEntityTypeConfiguration<GameCar>
{
    public void Configure(EntityTypeBuilder<GameCar> builder)
    {
        builder.Property(x => x.Password).IsRequired().HasMaxLength(40);
    }
}
