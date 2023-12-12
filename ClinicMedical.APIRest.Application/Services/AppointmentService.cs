using AutoMapper;
using ClinicMedical.APIRest.Application.DTOs;
using ClinicMedical.APIRest.Application.Interfaces;
using ClinicMedical.APIRest.Domain.Entities;
using ClinicMedical.APIRest.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<AppointmentDTO> AddAppointment(AppointmentDTO appointmentDTO)
        {
            var appointment = _mapper.Map<AppointmentEntity>(appointmentDTO);
            var appointmentAdded = await _appointmentRepository.AddAppointment(appointment);
            return _mapper.Map<AppointmentDTO>(appointmentAdded);
        }

        public async Task<bool> DeleteAppointment(int id)
        {
            var appointmentDeleted = await _appointmentRepository.DeleteAppointment(id);
            return _mapper.Map<bool>(appointmentDeleted);
        }

        public async Task<List<AppointmentDTO>> GetAllAppointments()
        {
            var appointments = await _appointmentRepository.GetAllAppointmensts();
            return _mapper.Map<List<AppointmentDTO>>(appointments);
        }

        public async Task<AppointmentDTO> GetAppointmentById(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(id);
            return _mapper.Map<AppointmentDTO>(appointment);
        }

        public async Task<AppointmentDTO> UpdateAppointment(AppointmentDTO appointmentDTO, int id)
        {
            var appointment = _mapper.Map<AppointmentEntity>(appointmentDTO);
            var appointmentUpdated = await _appointmentRepository.UpdateAppointment(appointment, id);
            return _mapper.Map<AppointmentDTO>(appointmentUpdated);
        }
    }
}
