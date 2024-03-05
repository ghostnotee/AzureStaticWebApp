using Domain.AnamnesisForms;
using Domain.Users;
using Domain.Users.Entities;
using Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace Infrastructure.Persistence.Configurations;

public class ClientConfigurations : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.CreatedDate).ValueGeneratedOnAdd();
        builder.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
        builder.HasOne<AnamnesisForm>().WithOne().HasForeignKey<Client>(c => c.AnamnesisFormId);
        builder.HasOne<User>().WithMany().HasForeignKey(c => c.ExpertId);
        builder.Property(x => x.FirstName).HasMaxLength(50);
        builder.Property(x => x.MiddleName).HasMaxLength(50);
        builder.Property(x => x.LastName).HasMaxLength(70);
        builder.Property(x => x.ParentName).HasMaxLength(50);
        builder.Property(x => x.ParentLastName).HasMaxLength(50);
        builder.Property(x => x.IdentityNumber).HasMaxLength(11);
        builder.Property(x => x.PhoneNumber).HasMaxLength(10);
        builder.Property(x => x.AdulthoodStage).HasConversion(v => v.ToString(), v => new AdulthoodStage(v)).HasMaxLength(15);
        builder.HasMany(c => c.Appointments).WithOne().HasForeignKey(a => a.ClientId);
        builder.HasMany<Address>(x => x.Addreses).WithOne().HasForeignKey(x => x.ClientId);
    }
}