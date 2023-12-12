using ClinicMedical.APIRest.Domain.Enums;

namespace ClinicMedical.APIRest.Domain.Entities
{
    public class AppointmentEntity
    {
        public int Id { get; set; }
        public StatusAppointmentEnum? Status { get; set; }
        public required string DateAdd { get; set; }
        public required string HoursAdd { get; set; }
        public string? DateReturn { get; set; }
        public string? HoursReturn { get; set; }
        public required int PatientId { get; set; }
        public virtual PatientEntity? Patient { get; set; }
        public required int DoctorId { get; set; }
        public virtual DoctorEntity? Doctor { get; set; }
    }
}
