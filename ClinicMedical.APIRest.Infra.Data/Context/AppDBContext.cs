using ClinicMedical.APIRest.Domain.Entities;
using ClinicMedical.APIRest.Infra.Data.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace ClinicMedical.APIRest.Infra.Data.Context
{    
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {            
        }

        public DbSet<DoctorEntity> Doctors { get; set; }
        public DbSet<PatientEntity> Patients { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<AppointmentEntity> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
        }
    }
}
