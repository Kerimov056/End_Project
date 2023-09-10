using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Configurations;

public class CommunicationConfigurations : IEntityTypeConfiguration<Communication>
{
    public void Configure(EntityTypeBuilder<Communication> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.SurName).IsRequired().HasMaxLength(140);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Phone).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Note).IsRequired().HasMaxLength(3000);
    }
}
