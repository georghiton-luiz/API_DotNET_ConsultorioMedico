using ClinicMedical.APIRest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMedical.APIRest.Infra.Data.EntitiesConfiguration
{
    public class PatientMap : IEntityTypeConfiguration<PatientEntity>
    {
        public void Configure(EntityTypeBuilder<PatientEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(75);
            builder.Property(x => x.CPF_CNPJ).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(75);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(15);
            builder.Property(x => x.Login).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Perfil).HasMaxLength(15);
            builder.Property(x => x.Created).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Updated).HasMaxLength(50);
            builder.Property(x => x.HealthInsurance).IsRequired().HasMaxLength(50);
        }
    }
}
