using ClinicMedical.APIRest.Domain.Entities;
using ClinicMedical.APIRest.Domain.Enums;
using ClinicMedical.APIRest.Domain.Interface;
using ClinicMedical.APIRest.Domain.Interfaces;
using ClinicMedical.APIRest.Domain.Services;
using ClinicMedical.APIRest.Infra.Data.Context;
using CpfCnpjLibrary;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ClinicMedical.APIRest.Infra.Data.Repository
{
    public class PatientRepositry : IPatientRepository
    {
        private readonly AppDBContext _dbContext;
        private readonly IFormatterService _formatterService;
        public PatientRepositry(AppDBContext appointmentsDBContex, IFormatterService formatter)
        {
            _dbContext = appointmentsDBContex;
            _formatterService = formatter;
        }
        public async Task<PatientEntity> GetPatientById(int id)
        {
            return await _dbContext.Patients.FirstOrDefaultAsync(patient => patient.Id == id);
        }
        public async Task<List<PatientEntity>> GetAllPatients()
        {
            return await _dbContext.Patients.ToListAsync();
        }
        public async Task<PatientEntity> AddPatient(PatientEntity patient)
        {

            DateTime currentDate = DateTime.Now;

            patient.CPF_CNPJ = _formatterService.IsCPF_CNPJ(patient.CPF_CNPJ);
            patient.Created = currentDate.ToString("dd/MM/yyyy");
            patient.Updated = null;
            patient.Phone = _formatterService.FormatPhoneNumber(patient.Phone);
            patient.Perfil = PerfilEnum.Paciente;         

            await _dbContext.Patients.AddAsync(patient);
            await _dbContext.SaveChangesAsync();
            return patient;
        }
        public async Task<PatientEntity> UpdatePatient(PatientEntity patient, int id)
        {
            PatientEntity patientById = await GetPatientById(id);
            if (patientById == null)
            {
                throw new Exception("Paciente não foi encontrado.");
            }
            DateTime currentDate = DateTime.Now;

            patientById.Name = patient.Name;            
            patientById.Email = patient.Email;
            patientById.Phone = _formatterService.FormatPhoneNumber(patient.Phone);
            patientById.Login = patient.Login;
            patientById.Updated = currentDate.ToString("dd/MM/yyyy");
            patientById.HealthInsurance = patient.HealthInsurance;

            _dbContext.Patients.Update(patientById);
            await _dbContext.SaveChangesAsync();

            return patientById;
        }
        public async Task<bool> DeletePatient(int id)
        {
            PatientEntity patientById = await GetPatientById(id);
            if (patientById == null)
            {
                throw new Exception($"Paciente para o ID: {id} não foi encontrado.");
            }

            _dbContext.Patients.Remove(patientById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
