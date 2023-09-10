using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Configurations;

public class CarReservationConficurations : IEntityTypeConfiguration<CarReservation>
{
    public void Configure(EntityTypeBuilder<CarReservation> builder)
    {
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(140);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Number).IsRequired().HasMaxLength(100);
        //builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(122000);
        builder.Property(x => x.Notes).IsRequired().HasMaxLength(12000);
    }
}
