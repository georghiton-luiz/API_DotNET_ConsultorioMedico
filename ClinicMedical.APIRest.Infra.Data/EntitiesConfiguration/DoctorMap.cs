using ClinicMedical.APIRest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMedical.APIRest.Infra.Data.EntitiesConfiguration
{
    public class DoctorMap : IEntityTypeConfiguration<DoctorEntity>
    {
        public void Configure(EntityTypeBuilder<DoctorEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CPF_CNPJ).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.Created).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Updated).HasMaxLength(50);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(15);
            builder.Property(x => x.Login).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Credentials).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Specialty).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Perfil).HasMaxLength(15);

        }
    }
}

