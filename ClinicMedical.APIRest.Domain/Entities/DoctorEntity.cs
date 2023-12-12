using ClinicMedical.APIRest.Domain.Enums;

namespace ClinicMedical.APIRest.Domain.Entities
{
    public class DoctorEntity : User
    {
        public required string Credentials { get; set; }
        public required string Specialty { get; set; }
    }
}
