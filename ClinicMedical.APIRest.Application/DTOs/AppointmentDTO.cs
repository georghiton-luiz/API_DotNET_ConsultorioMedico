using ClinicMedical.APIRest.Domain.Entities;
using ClinicMedical.APIRest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Application.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public StatusAppointmentEnum Status { get; set; }
        public required string DateAdd { get; set; }
        public required string HoursAdd { get; set; }
        public string? DateReturn { get; set; }
        public string? HoursReturn { get; set; }
        public required int PatientId { get; set; }
        public required int DoctorId { get; set; }
    }
}
