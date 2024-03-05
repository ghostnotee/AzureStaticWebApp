using Domain.Appointments.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OpinionConfigurations : IEntityTypeConfiguration<Opinion>
{
    public void Configure(EntityTypeBuilder<Opinion> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.CreatedDate).ValueGeneratedOnAdd();
        builder.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
        builder.Property(x => x.OpinionDescription).HasMaxLength(500);
    }
}