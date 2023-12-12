using ClinicMedical.APIRest.Domain.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Numerics;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace ClinicMedical.APIRest.Domain.Entities
{
    public class PatientEntity : User
    {
        public required string HealthInsurance { get; set; }
    }
}
