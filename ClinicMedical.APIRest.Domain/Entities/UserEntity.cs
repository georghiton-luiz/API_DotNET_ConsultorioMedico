using ClinicMedical.APIRest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Domain.Entities
{
    public abstract class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public required string CPF_CNPJ { get; set; }
        public string? Created { get; set; }
        public string? Updated { get; set; }
        public required string Phone { get; set; }
        public required string Login { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public required PerfilEnum Perfil { get; set; }
    }
}
