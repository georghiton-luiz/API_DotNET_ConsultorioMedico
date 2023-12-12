using ClinicMedical.APIRest.Application.DTOs;
using ClinicMedical.APIRest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<List<DoctorDTO>> GetAllDoctors();
        Task<DoctorDTO> GetDoctorById(int id);
        Task<DoctorDTO> AddDoctor(DoctorDTO doctorDTO);
        Task<DoctorDTO> UpdateDoctor(DoctorDTO doctorDTO, int id);
        Task<bool> DeleteDoctor(int id);
    }
}
