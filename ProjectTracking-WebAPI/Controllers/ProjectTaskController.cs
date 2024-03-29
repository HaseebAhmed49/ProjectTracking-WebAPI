﻿using System;
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
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    public class ProjectTaskController : Controller
    {
        private ProjectTaskService _service;
        private readonly IJWTManagerInterface _JWTManager;

        public ProjectTaskController(ProjectTaskService service, IJWTManagerInterface jWTManager)
        {
            _service = service;
            _JWTManager = jWTManager;
        }

        // GET: api/values
        [HttpGet("get-all-project-tasks")]
        public async Task<IActionResult> GetAllProjectTasks()
        {
            try
            {
                var allProjectTasks = await _service.GetAllProjectTasks();
                return (allProjectTasks != null) ? Ok(allProjectTasks) : BadRequest("No Project Tasks Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // GET: api/values
        [HttpGet("get-all-project-tasks-with-details")]
        public async Task<IActionResult> GetAllProjectTasksWithDetails()
        {
            try
            {
                var allProjectTasks = await _service.GetAllProjectTasksWithDetails();
                return (allProjectTasks != null) ? Ok(allProjectTasks) : BadRequest("No Project Tasks Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-project-task-for-employee-by-id/{id}")]
        public async Task<IActionResult> GetProjectTaskForEmployeeById(int id)
        {
            try
            {
                var projectTasksForAnEmployee = await _service.GetAllProjectTasksForAnEmployee(id);
                return (projectTasksForAnEmployee != null) ? Ok(projectTasksForAnEmployee) : BadRequest($"No Project Task for Employee Id:{id} Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: api/values/5
        [HttpGet("get-project-task-by-id/{id}")]
        public async Task<IActionResult> GetProjectTaskById(int id)
        {
            try
            {
                var projectTask = await _service.GetProjectTaskById(id);
                return (projectTask != null) ? Ok(projectTask) : BadRequest($"No Project Task with Id:{id} Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/values/5
        [HttpGet("get-project-task-with-details-by-id/{id}")]
        public async Task<IActionResult> GetProjectTaskWithDetailsById(int id)
        {
            try
            {
                var projectTask = await _service.GetProjectTaskWithDetails(id);
                return (projectTask != null) ? Ok(projectTask) : BadRequest($"No Project Task with Id:{id} Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/values
        [HttpPost("add-project-task")]
        public async Task<IActionResult> Post([FromBody] ProjectTaskVM projectTaskVM)
        {
            try
            {
                var newProjectTask = await _service.AddProjectTask(projectTaskVM);
                return Created($"api/projecttask/get-project-task-by-id/{newProjectTask.ProjectTaskID}", newProjectTask);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("update-project-task-by-id/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProjectTaskVM projectTaskVM)
        {
            try
            {
                var updatedProjectTask = await _service.UpdateProjectTaskById(id,projectTaskVM);
                return Created($"api/projecttask/get-project-task-by-id/{updatedProjectTask.ProjectTaskID}", updatedProjectTask);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("delete-project-task-by-id/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var updatedProjectTask = await _service.DeleteProjectTaskById(id);
                return Created($"api/projecttask/get-project-task-by-id/{updatedProjectTask.ProjectTaskID}", updatedProjectTask);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

