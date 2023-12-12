using ClinicMedical.APIRest.Domain.Entities;
using ClinicMedical.APIRest.Domain.Enums;
using ClinicMedical.APIRest.Domain.Interface;
using ClinicMedical.APIRest.Domain.Interfaces;
using ClinicMedical.APIRest.Domain.Services;
using ClinicMedical.APIRest.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace ClinicMedical.APIRest.Infra.Data.Repository
{
    public class AppointmentRepositry : IAppointmentRepository
    {
        private readonly AppDBContext _dbContext;
        private readonly IFormatterService _formatterService;

        public AppointmentRepositry(AppDBContext dbContext, IFormatterService formatter)
        {
            _dbContext = dbContext;
            _formatterService = formatter;
        }

        public async Task<AppointmentEntity> GetAppointmentById(int id)
        {
            return await _dbContext.Appointments.Include(x => x.Patient).Include(x => x.Doctor)
                .FirstOrDefaultAsync(appointment => appointment.Id == id);
        }
        public async Task<List<AppointmentEntity>> GetAllAppointmensts()
        {
            return await _dbContext.Appointments.Include(x => x.Patient).Include(x => x.Doctor)
                .ToListAsync();
        }
        public async Task<AppointmentEntity> AddAppointment(AppointmentEntity appointment)
        {
            appointment.DateAdd = _formatterService.ConvertToDateFormat(appointment.DateAdd);
            appointment.HoursAdd = _formatterService.FormatTime(appointment.HoursAdd);
            appointment.DateReturn = null;
            appointment.HoursReturn = null;
            await _dbContext.Appointments.AddAsync(appointment);
            await _dbContext.SaveChangesAsync();
            appointment.Status = StatusAppointmentEnum.Agendado;


            return appointment;
        }
        public async Task<AppointmentEntity> UpdateAppointment(AppointmentEntity appointment, int id)
        {
            AppointmentEntity appointmentById = await GetAppointmentById(appointment.Id);
            if (appointmentById == null)
            {
                throw new Exception("Consulta não encontrado.");
            }

            appointmentById.Status = appointment.Status;
            appointmentById.PatientId = appointment.PatientId;
            appointmentById.DoctorId = appointment.DoctorId;
            appointmentById.DateReturn = _formatterService.ConvertToDateFormat(appointment.DateReturn);
            appointmentById.HoursReturn = _formatterService.FormatTime(appointment.HoursAdd);

            _dbContext.Appointments.Update(appointmentById);
            await _dbContext.SaveChangesAsync();

            return appointmentById;
        }
        public async Task<bool> DeleteAppointment(int id)
        {
            AppointmentEntity appointmentById = await GetAppointmentById(id);
            if (appointmentById == null)
            {
                throw new Exception("Consulta não encontrada.");
            }

            _dbContext.Appointments.Remove(appointmentById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
