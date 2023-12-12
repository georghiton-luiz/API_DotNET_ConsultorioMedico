using ClinicMedical.APIRest.Domain.Acount;
using ClinicMedical.APIRest.Domain.Entities;
using ClinicMedical.APIRest.Domain.Interfaces;
using ClinicMedical.APIRest.Infra.Data.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppDBContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IFormatterService _formatterService;

        public AuthenticateService(AppDBContext dbContext, IConfiguration configuration, IFormatterService formatterService)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _formatterService = formatterService;
        }

        public async Task<bool> AuthenticateAsync(string login, string password)
        {
            User user = null;

            var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Login.ToLower() == login.ToLower());
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Login.ToLower() == login.ToLower());
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Login.ToLower() == login.ToLower());

            if (patient != null)
            {
                user = patient;
            }
            else if (doctor != null)
            {
                user = doctor;
            }
            else if (employee != null)
            {
                user = employee;
            }

            if (user == null)
            {
                return false;
            }


            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computerdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computerdHash.Length; i++)
            {
                if (computerdHash[i] != user.PasswordHash[i])
                {
                    return false;
                }
            }

            return true;
        }

        public string GenerationToken(int id, string login)
        {
            var claims = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("login", login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretKey"]));

            var credencials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(30);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credencials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> IsLoginExists(string login)
        {
            User user = null;

            var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Login.ToLower() == login.ToLower());
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Login.ToLower() == login.ToLower());
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Login.ToLower() == login.ToLower());

            if (patient != null)
            {
                user = patient;
            }
            else if (doctor != null)
            {
                user = doctor;
            }
            else if (employee != null)
            {
                user = employee;
            }

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UserExistsByCPF_CNPJ(string cPF_CNPJ)
        {
            var cPF_CNPJFormatted = _formatterService.IsCPF_CNPJ(cPF_CNPJ);

            User user = null;

            var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.CPF_CNPJ.ToLower() == cPF_CNPJ.ToLower());
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.CPF_CNPJ.ToLower() == cPF_CNPJ.ToLower());
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.CPF_CNPJ.ToLower() == cPF_CNPJ.ToLower());

            if (patient != null)
            {
                user = patient;
            }
            else if (doctor != null)
            {
                user = doctor;
            }
            else if (employee != null)
            {
                user = employee;
            }

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public async Task<PatientEntity> UserExistsByLogin(string login)
        {
            return await _dbContext.Patients.Where(p => p.Login.ToLower() == login.ToLower()).FirstOrDefaultAsync();
        }
    }
}
