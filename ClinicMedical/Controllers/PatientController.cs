using ClinicMedical.APIRest.Application.DTOs;
using ClinicMedical.APIRest.Application.Interfaces;
using ClinicMedical.APIRest.Application.Services;
using ClinicMedical.APIRest.Domain.Acount;
using ClinicMedical.APIRest.Domain.Entities;
using ClinicMedical.APIRest.Domain.Enums;
using ClinicMedical.APIRest.Infra.Ioc;
using ClinicMedical.APIRest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicMedical.APIRest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IPatientService _patientService;

        public PatientController(IAuthenticateService authenticateService, IPatientService patientService)
        {
            _authenticateService = authenticateService;
            _patientService = patientService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserToken>> AddPatient(PatientDTO patientDTO)
        {
            if (patientDTO == null)
            {
                return BadRequest("Dados inválidos");
            }

            var cPF_CNPJ = await _authenticateService.UserExistsByCPF_CNPJ(patientDTO.CPF_CNPJ);

            if (cPF_CNPJ)
            {
                return BadRequest("Esté CPF_CNPJ já possui um cadastro");
            }

            var login = await _authenticateService.IsLoginExists(patientDTO.Login);

            if (login)
            {
                return BadRequest("Esté Login já possui um cadastro");
            }

            patientDTO.Perfil = PerfilEnum.Paciente;
            var patient = await _patientService.AddPatient(patientDTO);

            if (patient == null)
            {
                return BadRequest("Ocorreu um erro ao cadastrar");
            }

            var token = _authenticateService.GenerationToken(patient.Id, patient.Login);

            return new UserToken
            {
                Token = token
            };
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdatePatient(PatientDTO patientDTO, int id)
        {
            var userId = User.GetId();
            var user = await _patientService.GetPatientById(userId);

            if (user.Perfil != PerfilEnum.Admin || user.Perfil != PerfilEnum.Funcionairo || user.Perfil != PerfilEnum.Paciente)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            var patientDTOUpdated = await _patientService.UpdatePatient(patientDTO, id);
            if (patientDTOUpdated == null)
            {
                return BadRequest("Ocorreu um erro ao alterar o Funcionário");
            }
            return Ok("Funcionario alterado com sucesso");
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeletePatient(int id)
        {
            var userId = User.GetId();
            var user = await _patientService.GetPatientById(userId);

            if (user.Perfil != PerfilEnum.Admin)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            var patientDTODeleted = await _patientService.DeletePatient(id);
            if (patientDTODeleted)
            {
                return BadRequest("Ocorreu um erro ao alterar o Funcionário");
            }
            return Ok("Funcionario excluido com sucesso");
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetPatientById(int id)
        {
            var userId = User.GetId();
            var user = await _patientService.GetPatientById(userId);

            if (user.Perfil != PerfilEnum.Admin || user.Perfil != PerfilEnum.Funcionairo || user.Perfil != PerfilEnum.Paciente)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            var patientDTO = await _patientService.GetPatientById(id);
            if (patientDTO == null)
            {
                return NotFound("Paciente não encontrado");
            }
            return Ok(patientDTO);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllPatients()
        {
            var userId = User.GetId();
            var user = await _patientService.GetPatientById(userId);

            if (user.Perfil != PerfilEnum.Admin || user.Perfil != PerfilEnum.Funcionairo)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            var patientsDTO = await _patientService.GetAllPatients();
            return Ok(patientsDTO);
        }
    }
}
