using System;
using ProjectTracking_WebAPI.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public interface IEmployeeInterface
    {
        public Task AddEmployee(EmployeeVM employee);

        public Task<List<Employee>> GetAllEmployee();

        public Task<Employee> GetEmployeeById(int id);

        public Task DeleteEmployeeById(int id);

        public Task<Employee> UpdateEmployeeById(int id, EmployeeVM employee);

        public Task<EmployeeWithProjectTasks> GetEmployeeWithProjectTasksById(int id);
    }
}

