using ClinicMedical.APIRest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Application.DTOs
{
    public class DoctorDTO : UserDTO
    {      
        public required string Credentials { get; set; }
        public required string Specialty { get; set; }
    }
}
