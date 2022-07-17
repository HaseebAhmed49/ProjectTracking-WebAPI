using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking_WebAPI.Data.Models;
using ProjectTracking_WebAPI.Data.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectTracking_WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IJWTManagerInterface _JWTManager;

        public UserController(IJWTManagerInterface jWTManager)
        {
            _JWTManager = jWTManager;
        }

        [HttpGet]
        public List<string> Get()
        {
            var users = new List<string>
            {
                "Haseeb Ahmed",
                "Umair Ahmed"
            };

            return users;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(Users users)
        {
            var token = _JWTManager.Auhtenticate(users);

            if(token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
           

    }
}

