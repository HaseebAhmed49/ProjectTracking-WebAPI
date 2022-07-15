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
    public class UserStoryController : Controller
    {
        private UserStoryService _service;

        public UserStoryController(UserStoryService service)
        {
            _service = service;
        }

        // GET: api/values
        [HttpGet("get-all-user-stories")]
        public async Task<IActionResult> GetAllUserStories()
        {
            try
            {
                var userStories = await _service.GetAllUserStories();
                return (userStories != null) ? Ok(userStories) : BadRequest("No User Story Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/values/5
        [HttpGet("get-user-story-with-project-details-by-id/{id}")]
        public async Task<IActionResult> GetUserStoryWithProjectDetailsById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest("User Story Id cant be -ve or zero");
                var userStory = await _service.GetUserStoryWithProjectDetails(id);
                return (userStory != null) ? Ok(userStory) : BadRequest("No User Story Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/values/5
        [HttpGet("get-all-user-story-by-id/{id}")]
        public async Task<IActionResult> GetUserStoryById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest("User Story Id cant be -ve or zero");
                var userStory = await _service.GetUserStoryById(id);
                return (userStory != null) ? Ok(userStory) : BadRequest("No User Story Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/values
        [HttpPost("add-user-story")]
        public async Task<IActionResult> AddUserStory([FromBody] UserStoryVM userStoryVM)
        {
            try
            {
                var newUserStory = await _service.AddUserStory(userStoryVM);
                return Created($"api/userstory/get-all-user-story-by-id/{newUserStory.UserStoryID}", newUserStory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("update-user-story-by-id/{id}")]
        public async Task<IActionResult> UpdateUserStoryById(int id,[FromBody] UserStoryVM userStoryVM)
        {
            try
            {
                if (id <= 0) return BadRequest("User Story Id cant be -ve or zero");
                var updatedUserStory = await _service.UpdateUserStoryById(id,userStoryVM);
                return Ok(updatedUserStory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("delete-user-story-by-id/{id}")]
        public async Task<IActionResult> DeleteUserStoryById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest("User Story Id cant be -ve or zero");
                var deletedUserStory = await _service.DeleteUserStoryById(id);
                return Ok(deletedUserStory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

