using ClinicMedical.APIRest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMedical.APIRest.Infra.Data.EntitiesConfiguration
{
    public class AppointmentMap : IEntityTypeConfiguration<AppointmentEntity>
    {
        public void Configure(EntityTypeBuilder<AppointmentEntity> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Status).HasMaxLength(50);
            builder.Property(a => a.DateAdd).IsRequired().HasMaxLength(50);
            builder.Property(a => a.HoursAdd).IsRequired().HasMaxLength(50);
            builder.Property(a => a.DateReturn).HasMaxLength(50);
            builder.Property(a => a.HoursReturn).HasMaxLength(50);
            builder.HasOne(a => a.Patient);
            builder.Property(x => x.PatientId);
            builder.HasOne(a => a.Doctor);
            builder.Property(x => x.DoctorId);
        }
    }
}

