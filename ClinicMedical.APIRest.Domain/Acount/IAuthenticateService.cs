using ClinicMedical.APIRest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Domain.Acount
{
    public interface IAuthenticateService
    {
        Task<bool>AuthenticateAsync(string login, string password);
        Task<bool> UserExistsByCPF_CNPJ(string cPF_CNPJ);
        Task<bool> IsLoginExists(string cPF_CNPJ);
        Task<PatientEntity> UserExistsByLogin(string login);
        public string GenerationToken(int id, string login);
    }
}
