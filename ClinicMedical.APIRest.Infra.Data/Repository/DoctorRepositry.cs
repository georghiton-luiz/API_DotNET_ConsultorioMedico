using ClinicMedical.APIRest.Domain.Entities;
using ClinicMedical.APIRest.Domain.Enums;
using ClinicMedical.APIRest.Domain.Interface;
using ClinicMedical.APIRest.Domain.Interfaces;
using ClinicMedical.APIRest.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicMedical.APIRest.Infra.Data.Repository
{
    public class DoctorRepositry : IDoctorRepository
    {
        private readonly AppDBContext _dbContext;
        private readonly IFormatterService _formatterService;

        public DoctorRepositry(AppDBContext dbContext, IFormatterService formatter)
        {
            _dbContext = dbContext;
            _formatterService = formatter;
        }

        public async Task<DoctorEntity> GetDoctorById(int id)
        {
            return await _dbContext.Doctors.FirstOrDefaultAsync(doctor => doctor.Id == id);

        }
        public async Task<List<DoctorEntity>> GetAllDoctors()
        {
            return await _dbContext.Doctors.ToListAsync();
        }
        public async Task<DoctorEntity> AddDoctor(DoctorEntity doctor)
        {
            DateTime currentDate = DateTime.Now;

            doctor.CPF_CNPJ = _formatterService.IsCPF_CNPJ(doctor.CPF_CNPJ);
            doctor.Created = currentDate.ToString("dd/MM/yyyy");
            doctor.Updated = null;
            doctor.Phone = _formatterService.FormatPhoneNumber(doctor.Phone);
            doctor.Perfil = PerfilEnum.Medico;

            await _dbContext.Doctors.AddAsync(doctor);
            await _dbContext.SaveChangesAsync();
            return doctor;
        }
        public async Task<DoctorEntity> UpdateDoctor(DoctorEntity doctor, int id)
        {
            DoctorEntity doctorById = await GetDoctorById(id);
            if (doctorById == null)
            {
                throw new Exception("Médico não foi encontrado.");
            }

            DateTime currentDate = DateTime.Now;

            doctorById.Name = doctor.Name;
            doctorById.Email = doctor.Email;
            doctorById.Created = doctor.Created;
            doctorById.Updated = currentDate.ToString("dd/MM/yyyy");
            doctorById.Specialty = doctor.Specialty;
            doctorById.Phone = _formatterService.FormatPhoneNumber(doctor.Phone);
            doctorById.Login = doctor.Login;
            doctorById.Credentials = doctor.Credentials;
            doctorById.Perfil = PerfilEnum.Medico;

            _dbContext.Doctors.Update(doctorById);
            await _dbContext.SaveChangesAsync();

            return doctorById;
        }
        public async Task<bool> DeleteDoctor(int id)
        {
            DoctorEntity doctorById = await GetDoctorById(id);
            if (doctorById == null)
            {
                throw new Exception("Médico não foi encontrado.");
            }

            _dbContext.Doctors.Remove(doctorById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
