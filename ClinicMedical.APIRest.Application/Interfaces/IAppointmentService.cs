using ClinicMedical.APIRest.Application.DTOs;
using ClinicMedical.APIRest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<AppointmentDTO>> GetAllAppointments();
        Task<AppointmentDTO> GetAppointmentById(int id);
        Task<AppointmentDTO> AddAppointment(AppointmentDTO appointmentDTO);
        Task<AppointmentDTO> UpdateAppointment(AppointmentDTO appointmentDTO, int id);
        Task<bool> DeleteAppointment(int id);
    }
}
