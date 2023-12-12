using CpfCnpjLibrary;
using Microsoft.EntityFrameworkCore;
using ClinicMedical.APIRest.Domain.Interface;
using ClinicMedical.APIRest.Infra.Data.Context;
using ClinicMedical.APIRest.Domain.Entities;
using ClinicMedical.APIRest.Domain.Services;
using System;
using System.Numerics;
using ClinicMedical.APIRest.Domain.Interfaces;
using ClinicMedical.APIRest.Domain.Enums;

namespace ClinicMedical.APIRest.Infra.Data.Repository
{
    public class EmployeeRepositry : IEmployeeRepository
    {
        private readonly AppDBContext _dbContext;
        private readonly IFormatterService _formatterService;
                public EmployeeRepositry(AppDBContext dbContext, IFormatterService formatter)
        {
            _dbContext = dbContext;
            _formatterService = formatter;
        }

        public async Task<EmployeeEntity> GetEmployeeById(int id)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(employee => employee.Id == id);

        }
        public async Task<List<EmployeeEntity>> GetAllEmployees()
        {
            return await _dbContext.Employees.ToListAsync();
        }
        public async Task<EmployeeEntity> AddEmployee(EmployeeEntity employee)
        {
            if (Cpf.Validar(employee.CPF_CNPJ))
            {
                employee.CPF_CNPJ = Cpf.FormatarComPontuacao(employee.CPF_CNPJ);
            }
            else
            {
                if (Cnpj.Validar(employee.CPF_CNPJ))
                {
                    employee.CPF_CNPJ = Cnpj.FormatarComPontuacao(employee.CPF_CNPJ);
                }
                else
                {
                    throw new Exception("CPF ou CNPJ inválidos");
                }
            }

            DateTime currentDate = DateTime.Now;

            employee.CPF_CNPJ = _formatterService.IsCPF_CNPJ(employee.CPF_CNPJ);
            employee.Created = currentDate.ToString("dd/MM/yyyy");
            employee.Updated = null;
            employee.Phone = _formatterService.FormatPhoneNumber(employee.Phone);

            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }
        public async Task<EmployeeEntity> UpdateEmployee(EmployeeEntity employee, int id)
        {
            EmployeeEntity employeeById = await GetEmployeeById(id);
            if (employeeById == null)
            {
                throw new Exception("Colaborador não foi encontrado.");
            }
            employeeById.Name = employee.Name;
            employeeById.Email = employee.Email;
            employeeById.Updated = _formatterService.ConvertToDateFormat(employee.Updated);
            employeeById.Phone = _formatterService.FormatPhoneNumber(employee.Phone);
            employeeById.Login = employee.Login;
            employeeById.Cargo = employee.Cargo;
            employeeById.Perfil = employee.Perfil;

            _dbContext.Employees.Update(employeeById);
            await _dbContext.SaveChangesAsync();

            return employeeById;
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            EmployeeEntity employeeById = await GetEmployeeById(id);
            if (employeeById == null)
            {
                throw new Exception("Paciente não foi encontrado.");
            }

            _dbContext.Employees.Remove(employeeById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
