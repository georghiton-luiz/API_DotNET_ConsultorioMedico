using ClinicMedical.APIRest.Application.DTOs;
using ClinicMedical.APIRest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Application.Interfaces
{
    public interface IEmployeeService
    {            
        Task<List<EmployeeDTO>> GetAllEmployees();
        Task<EmployeeDTO> GetEmployeeById(int id);
        Task<EmployeeDTO> AddEmployee(EmployeeDTO employeeDTO);
        Task<EmployeeDTO> UpdateEmployee(EmployeeDTO employeeDRO, int id);
        Task<bool> DeleteEmployee(int id);
    }
}

