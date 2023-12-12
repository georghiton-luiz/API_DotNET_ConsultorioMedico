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
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IAuthenticateService _authenticateService;
        private readonly IPatientService _patientService;
        private readonly IEmployeeService _employeeService;

        public DoctorController(IDoctorService doctorService, IAuthenticateService authenticateService, IPatientService patientService, IEmployeeService employeeService)
        {
            _doctorService = doctorService;
            _authenticateService = authenticateService;
            _patientService = patientService;
            _employeeService = employeeService;
        }

        [HttpPost("register")]
        [Authorize]
        public async Task<ActionResult<UserToken>> AddDoctor(DoctorDTO doctorDTO)
        {
            var userId = User.GetId();
            var patientUser = await _patientService.GetPatientById(userId);
            var doctortUser = await _doctorService.GetDoctorById(userId);
            var employeeUser = await _employeeService.GetEmployeeById(userId);

            if (patientUser.Perfil != PerfilEnum.Admin || doctortUser.Perfil != PerfilEnum.Admin || employeeUser.Perfil != PerfilEnum.Admin)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            if (doctorDTO == null)
            {
                return BadRequest("Dados inválidos");
            }

            var cPF_CNPJ = await _authenticateService.UserExistsByCPF_CNPJ(doctorDTO.CPF_CNPJ);

            if (cPF_CNPJ)
            {
                return BadRequest("Esté CPF_CNPJ já possui um cadastro");
            }

            var login = await _authenticateService.IsLoginExists(doctorDTO.Login);

            if (login)
            {
                return BadRequest("Esté Login já possui um cadastro");
            }

            var doctor = await _doctorService.AddDoctor(doctorDTO);

            if (doctor == null)
            {
                return BadRequest("Ocorreu um erro ao cadastrar");
            }

            var token = _authenticateService.GenerationToken(doctor.Id, doctor.Login);

            return new UserToken
            {
                Token = token
            };
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDoctor(DoctorDTO doctorDTO, int id)
        {
            var userId = User.GetId();
            var patientUser = await _patientService.GetPatientById(userId);
            var doctortUser = await _doctorService.GetDoctorById(userId);
            var employeeUser = await _employeeService.GetEmployeeById(userId);

            if (patientUser.Perfil != PerfilEnum.Admin || doctortUser.Perfil != PerfilEnum.Admin || employeeUser.Perfil != PerfilEnum.Admin)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            var doctorDTOUpdated = await _doctorService.UpdateDoctor(doctorDTO, id);
            if (doctorDTOUpdated == null)
            {
                return BadRequest("Ocorreu um erro ao alterar o Médico");
            }
            return Ok("Médico alterado com sucesso");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            var userId = User.GetId();
            var patientUser = await _patientService.GetPatientById(userId);
            var doctortUser = await _doctorService.GetDoctorById(userId);
            var employeeUser = await _employeeService.GetEmployeeById(userId);

            if (patientUser.Perfil != PerfilEnum.Admin || doctortUser.Perfil != PerfilEnum.Admin || employeeUser.Perfil != PerfilEnum.Admin)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            var doctorDTODeleted = await _doctorService.DeleteDoctor(id);
            if (doctorDTODeleted )
            {
                return BadRequest("Ocorreu um erro ao deletar o médico");
            }
            return Ok("Médico excluido com sucesso");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDoctorById(int id)
        {
            var userId = User.GetId();
            var patientUser = await _patientService.GetPatientById(userId);
            var doctortUser = await _doctorService.GetDoctorById(userId);
            var employeeUser = await _employeeService.GetEmployeeById(userId);

            if (patientUser.Perfil != PerfilEnum.Admin || doctortUser.Perfil != PerfilEnum.Medico || employeeUser.Perfil != PerfilEnum.Admin || employeeUser.Perfil != PerfilEnum.Funcionairo)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            var doctorDTO = await _doctorService.GetDoctorById(id);
            if (doctorDTO == null)
            {
                return NotFound("Médico não encontrado");
            }
            return Ok(doctorDTO);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDoctors()
        {
            var userId = User.GetId();
            var patientUser = await _patientService.GetPatientById(userId);
            var doctortUser = await _doctorService.GetDoctorById(userId);
            var employeeUser = await _employeeService.GetEmployeeById(userId);

            if (patientUser.Perfil != PerfilEnum.Admin || doctortUser.Perfil != PerfilEnum.Medico || employeeUser.Perfil != PerfilEnum.Admin || employeeUser.Perfil != PerfilEnum.Funcionairo)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            var doctorsDTO = await _doctorService.GetAllDoctors();
            return Ok(doctorsDTO);
        }
    }
}
