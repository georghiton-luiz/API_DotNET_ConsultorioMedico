using ClinicMedical.APIRest.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Application.Interfaces
{
    public interface IPatientService
    {
        Task<List<PatientDTO>> GetAllPatients();
        Task<PatientDTO> GetPatientById(int id);
        Task<PatientDTO> AddPatient(PatientDTO patientDTO);
        Task<PatientDTO> UpdatePatient(PatientDTO patientDTO, int id);
        Task<bool> DeletePatient(int id);
    }
}
