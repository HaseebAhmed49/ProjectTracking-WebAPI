using System;
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
    public class ProjectTaskController : Controller
    {
        private readonly ProjectTaskService _service;

        public ProjectTaskController(ProjectTaskService service)
        {
            _service = service;
        }

        // GET: api/values
        [HttpGet("get-all-project-tasks")]
        public async Task<IActionResult> GetAllProjectTasks()
        {
            try
            {
                var allProjectTasks = _service.GetAllProjectTasks();
                return (allProjectTasks != null) ? Ok(allProjectTasks) : BadRequest("No Project Tasks Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/values/5
        [HttpGet("get-all-project-tasks/{id}")]
        public async Task<IActionResult> GetProjectTaskById(int id)
        {
            try
            {
                var projectTask = _service.GetProjectTaskById(id);
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
                var newProjectTask = _service.AddProjectTask(projectTaskVM);
                return Ok(newProjectTask);
//                return Created($"api/project/get-project-by-id/{newProjectTask.}", addedProject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

