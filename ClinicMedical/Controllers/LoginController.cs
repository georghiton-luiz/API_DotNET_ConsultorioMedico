using ClinicMedical.APIRest.Application.DTOs;
using ClinicMedical.APIRest.Application.Interfaces;
using ClinicMedical.APIRest.Domain.Acount;
using ClinicMedical.APIRest.Domain.Entities;
using ClinicMedical.APIRest.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicMedical.APIRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public LoginController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> GetByLogin(LoginModel loginModel)
        {
            var exist = await _authenticateService.UserExistsByLogin(loginModel.Login);

            if (exist == null)
            {
                return Unauthorized("Paciente não existe");
            }

            var result = await _authenticateService.AuthenticateAsync(loginModel.Login, loginModel.Password);

            if (!result)
            {
                return Unauthorized("Usuário ou senha inválida");
            }

            User user = null;

            var patient = await _authenticateService.UserExistsByLogin(loginModel.Login);
            var doctor = await _authenticateService.UserExistsByLogin(loginModel.Login);
            var employee = await _authenticateService.UserExistsByLogin(loginModel.Login);

            if (patient != null)
            {
                user = patient;
            }
            else if (doctor != null)
            {
                user = doctor;
            }
            else if (employee != null)
            {
                user = employee;
            }

            var token = _authenticateService.GenerationToken(user.Id, user.Login);

            return new UserToken { Token = token };
        }
    }
}
