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
    public class UserDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public required string CPF_CNPJ { get; set; }

        [JsonIgnore]
        public string? Created { get; set; }

        [JsonIgnore]
        public string? Updated { get; set; }
        public required string Phone { get; set; }
        public required string Login { get; set; }
        [NotMapped]
        [Required]
        [MinLength(8, ErrorMessage = "A senha dever ter, no minimo, 8 caracteres")]
        public required string Password { get; set; }
        public required PerfilEnum Perfil { get; set; }
    }
}
