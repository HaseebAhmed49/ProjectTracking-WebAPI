using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking_WebAPI.Data.Services;
using ProjectTracking_WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectTracking_WebAPI.Controllers
{
    //    [Authorize]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        public EmployeeService _service;
        private readonly IJWTManagerInterface _JWTManager;

        public EmployeeController(EmployeeService service, IJWTManagerInterface jWTManager)
        {
            _service = service;
            _JWTManager = jWTManager;
        }

            [HttpGet("get-all-employees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            try
            {
                var allEmployees = await _service.GetAllEmployee();
                return (allEmployees != null) ? Ok(allEmployees) : BadRequest("No Employees Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-employee-by-id/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                if(id<0) return BadRequest($"Employee Id cant be -ve or 0");
                var employee = await _service.GetEmployeeById(id);
                return (employee != null) ? Ok(employee) : BadRequest($"No Employee with {id} Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-employee-with-project-tasks-by-id/{id}")]
        public async Task<IActionResult> GetEmployeeWithProjectTasksById(int id)
        {
            try
            {
                if (id < 0) return BadRequest($"Employee Id cant be -ve or 0");
                var employee = await _service.GetEmployeeWithProjectTasksById(id);
                return (employee != null) ? Ok(employee) : BadRequest($"No Employee with {id} Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-employee")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeVM employee)
        {
            try
            {
                var newEmployee = await _service.AddEmployee(employee);
                return Created($"api/employee/get-employee-by-id/{newEmployee.EmployeeID}",newEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-employee-by-id/{id}")]
        public async Task<IActionResult> UpdateEmployeeById(int id,[FromBody] EmployeeVM employee)
        {
            try
            {
                if (id < 0) return BadRequest($"Employee Id cant be -ve or 0");
                var updatedEmployee = await _service.UpdateEmployeeById(id, employee);
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-employee-by-id/{id}")]
        public async Task<IActionResult> DeleteEmployeeById(int id)
        {
            try
            {
                if (id < 0) return BadRequest($"Employee Id cant be -ve or 0");
                var deletedEmployee =  await _service.DeleteEmployeeById(id);
                return (deletedEmployee != null) ? Ok(deletedEmployee) : BadRequest("No Employee Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

