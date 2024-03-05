using Domain.AnamnesisForms;
using Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AnamnesisFormConfigurations : IEntityTypeConfiguration<AnamnesisForm>
{
    public void Configure(EntityTypeBuilder<AnamnesisForm> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.CreatedDate).ValueGeneratedOnAdd();
        builder.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
        builder.Property(x => x.Title).HasMaxLength(150);
        builder.HasMany(f => f.Questions).WithOne().HasForeignKey(q => q.AnamnesisFormId);
        builder.HasMany(f => f.Notes).WithOne().HasForeignKey(q => q.AnamnesisFormId);
        builder.HasOne<Client>().WithOne().HasForeignKey<AnamnesisForm>(x => x.ClientId);
    }
}