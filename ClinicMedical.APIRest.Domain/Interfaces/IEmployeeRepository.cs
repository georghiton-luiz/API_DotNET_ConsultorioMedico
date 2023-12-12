using ClinicMedical.APIRest.Domain.Entities;

namespace ClinicMedical.APIRest.Domain.Interface
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeEntity>> GetAllEmployees();
        Task<EmployeeEntity> GetEmployeeById(int id);
        Task<EmployeeEntity> AddEmployee(EmployeeEntity employee);
        Task<EmployeeEntity> UpdateEmployee(EmployeeEntity employee, int id);
        Task<bool> DeleteEmployee(int id);
    }
}
