using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking_WebAPI.Data.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectTracking_WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private ProjectServices _services;

        public ProjectsController(ProjectServices services)
        {
            _services = services;
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
                var allProjects = await _services.GetProjectById(id);
                return (allProjects != null) ? Ok(allProjects) : BadRequest($"No Project with ID:{id} Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

