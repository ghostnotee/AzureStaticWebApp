using Domain.Appointments;
using Domain.Appointments.ValueObjects;
using Domain.Users;
using Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AppointmentConfigurations : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.CreatedDate).ValueGeneratedOnAdd();
        builder.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
        builder.HasOne<Client>().WithMany(u => u.Appointments).HasForeignKey(a => a.ClientId);
        builder.HasOne<User>().WithMany().HasForeignKey(a => a.ExpertId);
        builder.Property(a => a.AppointmentState).HasConversion(s => s.ToString(), v => new AppointmentState(v)).HasMaxLength(50);
        builder.HasOne<Address>().WithMany().HasForeignKey(x => x.AddressId);
        builder.Property(a => a.Description).HasMaxLength(500);
        builder.HasMany(a => a.Opinions).WithOne().HasForeignKey(o => o.AppointmentId);
    }
}