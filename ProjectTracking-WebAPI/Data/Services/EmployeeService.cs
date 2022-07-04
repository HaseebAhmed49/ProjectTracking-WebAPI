using System;
using Microsoft.EntityFrameworkCore;
using ProjectTracking_WebAPI.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public class EmployeeService : IEmployeeInterface
    {
        private readonly AppDBContext _context;

        public EmployeeService(AppDBContext context)
        {
            _context = context;
        }

        public async Task AddEmployee(Employee employee)
        {
            Employee newEmployee = new Employee()
            {
                EmailID = employee.EmailID,
                SkillSets = employee.SkillSets,
                ContactNo = employee.ContactNo,
                Designation = employee.Designation,
                EmployeeName = employee.EmployeeName,
            };

            await _context.Employee.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeById(int id)
        {
            var employee = await _context.Employee.FirstOrDefaultAsync(em => em.EmoloyeeID == id);
            if(employee!=null)
            {
                _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Employee>> GetAllEmployee() => await _context.Employee.ToListAsync();

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await _context.Employee.FirstOrDefaultAsync(em => em.EmoloyeeID == id);
            return employee;
        }

        public async Task<Employee> UpdateEmployeeById(int id, Employee newEmployee)
        {
            var employee = await _context.Employee.FirstOrDefaultAsync(em => em.EmoloyeeID == id);
            if (employee != null)
            {
                employee.EmailID = newEmployee.EmailID;
                employee.SkillSets = newEmployee.SkillSets;
                employee.ContactNo = newEmployee.ContactNo;
                employee.Designation = newEmployee.Designation;
                employee.EmployeeName = newEmployee.EmployeeName;
                _context.Update(employee);
                await _context.SaveChangesAsync();
                return newEmployee;
            }
            return employee;
        }

    }
}

