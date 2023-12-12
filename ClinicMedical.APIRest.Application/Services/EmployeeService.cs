using AutoMapper;
using ClinicMedical.APIRest.Application.DTOs;
using ClinicMedical.APIRest.Application.Interfaces;
using ClinicMedical.APIRest.Domain.Entities;
using ClinicMedical.APIRest.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> AddEmployee(EmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<EmployeeEntity>(employeeDTO);

            if (employeeDTO.Password != null)
            {
                using var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(employeeDTO.Password));
                byte[] passwordSalt = hmac.Key;

                employee.PasswordHash = passwordHash;
                employee.PasswordSalt = passwordSalt;
            }

            var employeeAdded = await _employeeRepository.AddEmployee(employee);
            return _mapper.Map<EmployeeDTO>(employeeAdded);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employeeDeleted = await _employeeRepository.DeleteEmployee(id);
            return _mapper.Map<bool>(employeeDeleted);
        }

        public async Task<List<EmployeeDTO>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            return _mapper.Map<List<EmployeeDTO>>(employees);
        }

        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task<EmployeeDTO> UpdateEmployee(EmployeeDTO employeeDTO, int id)
        {
            var employee = _mapper.Map<EmployeeEntity>(employeeDTO);
            var employeeUpdated = await _employeeRepository.UpdateEmployee(employee, id);
            return _mapper.Map<EmployeeDTO>(employeeUpdated);
        }
    }
}
