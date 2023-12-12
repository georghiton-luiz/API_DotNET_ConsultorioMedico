using ClinicMedical.APIRest.Domain.Enums;

namespace ClinicMedical.APIRest.Domain.Entities
{
    public class EmployeeEntity : User
    {
        public required string Cargo { get; set; }
    }
}
