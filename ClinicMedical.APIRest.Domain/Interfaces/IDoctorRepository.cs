using ClinicMedical.APIRest.Domain.Entities;

namespace ClinicMedical.APIRest.Domain.Interface
{
    public interface IDoctorRepository
    {
        Task<List<DoctorEntity>> GetAllDoctors();
        Task<DoctorEntity> GetDoctorById(int id);
        Task<DoctorEntity> AddDoctor(DoctorEntity doctor);
        Task<DoctorEntity> UpdateDoctor(DoctorEntity doctor, int id);
        Task<bool> DeleteDoctor(int id);
    }
}
