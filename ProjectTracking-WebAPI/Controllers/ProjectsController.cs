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
    [Authorize]
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private ProjectServices _services;
        private readonly IJWTManagerInterface _JWTManager;

        public ProjectsController(ProjectServices services, IJWTManagerInterface jWTManager)
        {
            _services = services;
                _JWTManager = jWTManager;
        }

            [HttpGet("get-all-projects")]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var allProjects = await _services.GetAllProjects();
                return (allProjects != null) ? Ok(allProjects) : BadRequest("No Projects Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-project-by-id/{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            try
            {
                if (id < 0) return BadRequest("Project ID can't be -ve or 0");
                var allProjects = await _services.GetProjectById(id);
                return (allProjects != null) ? Ok(allProjects) : BadRequest($"No Project with ID:{id} Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-project-with-user-stories-by-id/{id}")]
        public async Task<IActionResult> GetProjectWithUserStoriesById(int id)
        {
            try
            {
                if (id < 0) return BadRequest("Project ID can't be -ve or 0");
                var project = await _services.GetProjectWithUserStoriesById(id);
                return (project != null) ? Ok(project) : BadRequest($"No Project with ID:{id} Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-project")]
        public async Task<IActionResult> AddProject([FromBody] ProjectVM projectVM)
        {
            try
            {
                var addedProject = await _services.AddProject(projectVM);
                return Created($"api/project/get-project-by-id/{addedProject.ProjectID}", addedProject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-project-by-id/{id}")]
        public async Task<IActionResult> UpdateProjectById(int id,[FromBody] ProjectVM projectVM)
        {
            try
            {
                if (id < 0) return BadRequest("Project ID can't be -ve or 0");
                var updatedProject = await _services.UpdateProjectById(id,projectVM);
                return Ok(updatedProject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-project-by-id/{id}")]
        public async Task<IActionResult> DeleteProjectById(int id)
        {
            try
            {
                if (id < 0) return BadRequest("Project ID can't be -ve or 0");
                var deletedProject = await _services.DeleteProjectById(id);
                return Ok(deletedProject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

