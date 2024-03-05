using Domain.AnamnesisForms.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AnamnesisFormQuestionConfigurations : IEntityTypeConfiguration<AnamnesisFormQuestion>
{
    public void Configure(EntityTypeBuilder<AnamnesisFormQuestion> builder)
    {
        builder.HasKey(q => q.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.CreatedDate).ValueGeneratedOnAdd();
        builder.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
    }
}