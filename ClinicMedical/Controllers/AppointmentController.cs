using ClinicMedical.APIRest.Application.DTOs;
using ClinicMedical.APIRest.Application.Interfaces;
using ClinicMedical.APIRest.Application.Services;
using ClinicMedical.APIRest.Domain.Acount;
using ClinicMedical.APIRest.Domain.Enums;
using ClinicMedical.APIRest.Infra.Ioc;
using ClinicMedical.APIRest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicMedical.APIRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAuthenticateService _authenticateService;
        private readonly IPatientService _petientService;
        private readonly IEmployeeService _employeeService;
        private readonly IDoctorService _doctorService;

        public AppointmentController(IAppointmentService appointmentService, IAuthenticateService authenticateService, IPatientService petientService, IEmployeeService employeeService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _authenticateService = authenticateService;
            _petientService = petientService;
            _employeeService = employeeService;
            _doctorService = doctorService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserToken>> AddAppointment(AppointmentDTO appointmentDTO)
        {
            var userId = User.GetId();
            var patientUser = await _petientService.GetPatientById(userId);
            var doctortUser = await _doctorService.GetDoctorById(userId);
            var employeeUser = await _employeeService.GetEmployeeById(userId);

            if (patientUser.Perfil != PerfilEnum.Paciente || doctortUser.Perfil != PerfilEnum.Admin || employeeUser.Perfil != PerfilEnum.Admin || employeeUser.Perfil != PerfilEnum.Funcionairo)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            var appointmentDTOAdded = await _appointmentService.AddAppointment(appointmentDTO);
            if (appointmentDTOAdded == null)
            {
                return BadRequest("Ocorreu um erro ao incluir uma consulta");
            }
            return Ok("Consulta agendada com sucesso!");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAppointment(AppointmentDTO appointmentDTO, int id)
        {
            var userId = User.GetId();
            var patientUser = await _petientService.GetPatientById(userId);
            var doctortUser = await _doctorService.GetDoctorById(userId);
            var employeeUser = await _employeeService.GetEmployeeById(userId);

            if (patientUser.Perfil != PerfilEnum.Admin || doctortUser.Perfil != PerfilEnum.Admin || employeeUser.Perfil != PerfilEnum.Admin)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            var appointmentDTOUpdated = await _appointmentService.UpdateAppointment(appointmentDTO, id);
            if (appointmentDTOUpdated == null)
            {
                return BadRequest("Ocorreu um erro ao alterar a consulta");
            }
            return Ok("Consulta alterado com sucesso");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAppointment(int id)
        {
            var userId = User.GetId();
            var patientUser = await _petientService.GetPatientById(userId);
            var doctortUser = await _doctorService.GetDoctorById(userId);
            var employeeUser = await _employeeService.GetEmployeeById(userId);

            if (patientUser.Perfil != PerfilEnum.Admin || doctortUser.Perfil != PerfilEnum.Admin || employeeUser.Perfil != PerfilEnum.Admin || employeeUser.Perfil != PerfilEnum.Funcionairo)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            var appointmentDTODeleted = await _appointmentService.DeleteAppointment(id);
            if (appointmentDTODeleted)
            {
                return BadRequest("Ocorreu um erro ao deletar uma consulta");
            }
            return Ok("Consulta excluido com sucesso");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAppointmentById(int id)
        {
            var appointmentDTO = await _appointmentService.GetAppointmentById(id);
            if (appointmentDTO == null)
            {
                return NotFound("Consulta não encontrada");
            }
            return Ok(appointmentDTO);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAppointments()
        {
            var appointmentsDTO = await _appointmentService.GetAllAppointments();
            return Ok(appointmentsDTO);
        }
    }
}
