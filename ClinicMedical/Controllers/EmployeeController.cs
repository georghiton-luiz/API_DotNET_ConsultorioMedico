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
    public class EmployeeController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IAuthenticateService authenticateService, IEmployeeService employeeService)
        {
            _authenticateService = authenticateService;
            _employeeService = employeeService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserToken>> AddEmployee(EmployeeDTO employeeDTO)
        {
            var employeeExist = await _employeeService.GetAllEmployees();
            //Se não houver funcionario registrado, o primeiro registro sera por padrão Admin
            if (employeeExist != null)
            {
                if (employeeDTO == null)
                {
                    return BadRequest("Dados inválidos");
                }

                var cPF_CNPJ = await _authenticateService.UserExistsByCPF_CNPJ(employeeDTO.CPF_CNPJ);

                if (cPF_CNPJ)
                {
                    return BadRequest("Esté CPF_CNPJ já possui um cadastro");
                }

                var login = await _authenticateService.IsLoginExists(employeeDTO.Login);

                if (login)
                {
                    return BadRequest("Esté Login já possui um cadastro");
                }

                employeeDTO.Perfil = PerfilEnum.Admin;
                var employee = await _employeeService.AddEmployee(employeeDTO);

                if (employee == null)
                {
                    return BadRequest("Ocorreu um erro ao cadastrar");
                }

                var token = _authenticateService.GenerationToken(employee.Id, employee.Login);

                return new UserToken
                {
                    Token = token
                };
            }
            else
            {
                //Depois do primeiro registro, apenas funcionairos admin podem registrar ou registra outros funcionarios
                var userId = User.GetId();
                var user = await _employeeService.GetEmployeeById(userId);

                if (user.Perfil != PerfilEnum.Admin)
                {
                    return Unauthorized("Você não tem permissão para excluir clientes");
                }

                if (employeeDTO == null)
                {
                    return BadRequest("Dados inválidos");
                }

                var cPF_CNPJ = await _authenticateService.UserExistsByCPF_CNPJ(employeeDTO.CPF_CNPJ);

                if (cPF_CNPJ)
                {
                    return BadRequest("Esté CPF_CNPJ já possui um cadastro");
                }

                var login = await _authenticateService.IsLoginExists(employeeDTO.Login);

                if (login)
                {
                    return BadRequest("Esté Login já possui um cadastro");
                }

                employeeDTO.Perfil = PerfilEnum.Admin;
                var employee = await _employeeService.AddEmployee(employeeDTO);

                if (employee == null)
                {
                    return BadRequest("Ocorreu um erro ao cadastrar");
                }

                var token = _authenticateService.GenerationToken(employee.Id, employee.Login);

                return new UserToken
                {
                    Token = token
                };
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateEmployee(EmployeeDTO employeeDTO, int id)
        {
            var userId = User.GetId();
            var user = await _employeeService.GetEmployeeById(userId);

            if (user.Perfil != PerfilEnum.Admin)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            var employeeDTOUpdated = await _employeeService.UpdateEmployee(employeeDTO, id);
            if (employeeDTOUpdated == null)
            {
                return BadRequest("Ocorreu um erro ao alterar o Funcionário");
            }
            return Ok("Funcionario alterado com sucesso");
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var userId = User.GetId();
            var user = await _employeeService.GetEmployeeById(userId);

            if (user.Perfil != PerfilEnum.Admin)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            var employeeDTODeleted = await _employeeService.DeleteEmployee(id);
            if (employeeDTODeleted)
            {
                return BadRequest("Ocorreu um erro ao alterar o Funcionário");
            }
            return Ok("Funcionario excluido com sucesso");
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetEmployeeById(int id)
        {
            var userId = User.GetId();
            var user = await _employeeService.GetEmployeeById(userId);

            if (user.Perfil != PerfilEnum.Admin)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            var employeeDTO = await _employeeService.GetEmployeeById(id);
            if (employeeDTO == null)
            {
                return NotFound("Paciente não encontrado");
            }
            return Ok(employeeDTO);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllEmployees()
        {
            var userId = User.GetId();
            var user = await _employeeService.GetEmployeeById(userId);

            if (user.Perfil != PerfilEnum.Admin)
            {
                return Unauthorized("Você não tem permissão para excluir clientes");
            }

            var employeesDTO = await _employeeService.GetAllEmployees();
            return Ok(employeesDTO);
        }
    }
}
