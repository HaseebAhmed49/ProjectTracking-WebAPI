using System;
using ProjectTracking_WebAPI.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public interface IEmployeeInterface
    {
        public void AddEmployee(Employee employee);

        public List<Employee> GetAllEmployee();

        public Employee GetEmployeeById(int id);
    }
}

