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

        public async Task<Employee> AddEmployee(EmployeeVM employee)
        {
            var newEmployee = new Employee()
            {
                EmailID = employee.EmailID,
                SkillSets = employee.SkillSets,
                ContactNo = employee.ContactNo,
                Designation = employee.Designation,
                EmployeeName = employee.EmployeeName,
            };

            await _context.Employee.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
            return newEmployee;
        }

        public async Task<Employee> DeleteEmployeeById(int id)
        {
            var employee = await _context.Employee.FirstOrDefaultAsync(em => em.EmoloyeeID == id);
            if(employee!=null)
            {
                _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();
            }
            return employee;
        }

        public async Task<List<Employee>> GetAllEmployee() => await _context.Employee.ToListAsync();

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await _context.Employee.FirstOrDefaultAsync(em => em.EmoloyeeID == id);
            return employee;
        }

        public async Task<EmployeeWithProjectTasks> GetEmployeeWithProjectTasksById(int id)
        {
            var employee = _context.Employee.Where(em => em.EmoloyeeID == id).Select(emp => new EmployeeWithProjectTasks()
            {
                EmployeeName = emp.EmployeeName,
                EmailID = emp.EmailID,
                ContactNo = emp.ContactNo,
                Designation = emp.Designation,
                SkillSets = emp.SkillSets,
                ProjectTasks = emp.ProjectTasks.Where(em => em.EmployeeID == id).Select(pt => new ProjectTaskForEmployeeVM()
                {
                    TaskEndDate = pt.TaskEndDate,
                    AssignedTo = pt.AssignedTo,
                    TaskStartDate = pt.TaskStartDate,
                    ProjectTaskID = pt.ProjectTaskID,
                    TaskCompletion = pt.TaskCompletion
                }).ToList()
            }).FirstOrDefault();
            return employee;
        }

        public async Task<Employee> UpdateEmployeeById(int id, EmployeeVM newEmployee)
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
            }
            return employee;
        }

    }
}

