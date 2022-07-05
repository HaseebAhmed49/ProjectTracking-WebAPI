﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking_WebAPI.Data.Services;
using ProjectTracking_WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectTracking_WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        public EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        [HttpGet("get-all-employees")]
        public async Task<IActionResult> GetAllEmployees()
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
                var newEmployee = _service.AddEmployee(employee);
                return Ok();
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
                await _service.DeleteEmployeeById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

