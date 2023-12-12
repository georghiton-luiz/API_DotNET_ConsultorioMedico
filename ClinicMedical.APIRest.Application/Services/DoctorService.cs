using AutoMapper;
using ClinicMedical.APIRest.Application.DTOs;
using ClinicMedical.APIRest.Application.Interfaces;
using ClinicMedical.APIRest.Domain.Entities;
using ClinicMedical.APIRest.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorService(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<DoctorDTO> AddDoctor(DoctorDTO doctorDTO)
        {
            var doctor = _mapper.Map<DoctorEntity>(doctorDTO);

            if (doctorDTO.Password != null)
            {
                using var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(doctorDTO.Password));
                byte[] passwordSalt = hmac.Key;

                doctor.PasswordHash = passwordHash;
                doctor.PasswordSalt = passwordSalt;
            }

            var doctorAdded = await _doctorRepository.AddDoctor(doctor);
            return _mapper.Map<DoctorDTO>(doctorAdded);
        }

        public async Task<bool> DeleteDoctor(int id)
        {
            var doctorDeleted = await _doctorRepository.DeleteDoctor(id);
            return _mapper.Map<bool>(doctorDeleted);
        }

        public async Task<List<DoctorDTO>> GetAllDoctors()
        {
            var doctors = await _doctorRepository.GetAllDoctors();
            return _mapper.Map<List<DoctorDTO>>(doctors);
        }

        public async Task<DoctorDTO> GetDoctorById(int id)
        {
            var doctor = await _doctorRepository.GetDoctorById(id);
            return _mapper.Map<DoctorDTO>(doctor);
        }

        public async Task<DoctorDTO> UpdateDoctor(DoctorDTO doctorDTO, int id)
        {
            var doctor = _mapper.Map<DoctorEntity>(doctorDTO);
            var doctorUpdated = await _doctorRepository.UpdateDoctor(doctor, id);
            return _mapper.Map<DoctorDTO>(doctorUpdated);
        }
    }
}
