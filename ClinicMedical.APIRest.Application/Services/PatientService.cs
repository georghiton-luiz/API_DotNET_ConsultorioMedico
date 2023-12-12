using AutoMapper;
using ClinicMedical.APIRest.Application.DTOs;
using ClinicMedical.APIRest.Application.Interfaces;
using ClinicMedical.APIRest.Domain.Entities;
using ClinicMedical.APIRest.Domain.Interface;
using ClinicMedical.APIRest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<PatientDTO> AddPatient(PatientDTO patientDTO)
        {
            var patient = _mapper.Map<PatientEntity>(patientDTO);

            if (patientDTO.Password != null)
            {
                using var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(patientDTO.Password));
                byte[] passwordSalt = hmac.Key;

                patient.PasswordHash = passwordHash;
                patient.PasswordSalt = passwordSalt;
            }

            var patientAdded = await _patientRepository.AddPatient(patient);
            return _mapper.Map<PatientDTO>(patientAdded);
        }

        public async Task<bool> DeletePatient(int id)
        {
            var patientDeleted = await _patientRepository.DeletePatient(id);
            return _mapper.Map<bool>(patientDeleted);
        }

        public async Task<List<PatientDTO>> GetAllPatients()
        {
            var patients = await _patientRepository.GetAllPatients();
            return _mapper.Map<List<PatientDTO>>(patients);
        }

        public async Task<PatientDTO> GetPatientById(int id)
        {
            var patient = await _patientRepository.GetPatientById(id);
            return _mapper.Map<PatientDTO>(patient);
        }

        public async Task<PatientDTO> UpdatePatient(PatientDTO patientDTO, int id)
        {
            var patient = _mapper.Map<PatientEntity>(patientDTO);
            var patientUpdated = await _patientRepository.UpdatePatient(patient, id);
            return _mapper.Map<PatientDTO>(patientUpdated);
        }
    }
}
