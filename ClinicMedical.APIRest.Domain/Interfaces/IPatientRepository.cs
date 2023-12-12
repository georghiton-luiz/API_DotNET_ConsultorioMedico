using ClinicMedical.APIRest.Domain.Entities;

namespace ClinicMedical.APIRest.Domain.Interface
{
    public interface IPatientRepository
    {
        Task<List<PatientEntity>> GetAllPatients();
        Task<PatientEntity> GetPatientById(int id);
        Task<PatientEntity> AddPatient(PatientEntity patient);
        Task<PatientEntity> UpdatePatient(PatientEntity patient, int id);
        Task<bool> DeletePatient(int id);
    }
}
