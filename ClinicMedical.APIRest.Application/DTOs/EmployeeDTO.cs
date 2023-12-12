using ClinicMedical.APIRest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ClinicMedical.APIRest.Application.DTOs
{
    public class EmployeeDTO : UserDTO
    {
        public required string Cargo { get; set; }
    }
}
