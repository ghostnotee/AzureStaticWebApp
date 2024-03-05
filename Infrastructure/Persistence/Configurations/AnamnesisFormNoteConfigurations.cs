using Domain.AnamnesisForms.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AnamnesisFormNoteConfigurations : IEntityTypeConfiguration<AnamnesisFormNote>
{
    public void Configure(EntityTypeBuilder<AnamnesisFormNote> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.CreatedDate).ValueGeneratedOnAdd();
        builder.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
    }
}