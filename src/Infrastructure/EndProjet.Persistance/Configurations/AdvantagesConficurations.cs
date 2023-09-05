using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Configurations;

public class AdvantagesConficurations : IEntityTypeConfiguration<Faq>
{
    public void Configure(EntityTypeBuilder<Faq> builder)
    {
        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Descrption).IsRequired().HasMaxLength(1000);
    }
}
