using ClinicMedical.APIRest.Domain.Entities;

namespace ClinicMedical.APIRest.Domain.Interface
{
    public interface IAppointmentRepository
    {
        Task<List<AppointmentEntity>> GetAllAppointmensts();
        Task<AppointmentEntity> GetAppointmentById(int id);
        Task<AppointmentEntity> AddAppointment(AppointmentEntity appointment);
        Task<AppointmentEntity> UpdateAppointment(AppointmentEntity appointment, int id);
        Task<bool> DeleteAppointment(int id);
    }
}
