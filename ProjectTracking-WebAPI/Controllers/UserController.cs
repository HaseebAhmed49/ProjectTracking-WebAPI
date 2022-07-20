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
        private readonly IUserServiceInterface _userServiceInterface;

        public UserController(IJWTManagerInterface jWTManager, IUserServiceInterface userServiceInterface)
        {
            _JWTManager = jWTManager;
            _userServiceInterface = userServiceInterface;
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
        public async Task<IActionResult> Authenticate(Users users)
        {
            var validUser = await _userServiceInterface.IsValidUserAsync(users);
            if (!validUser) return Unauthorized("Incorrect username or password");

            var token = _JWTManager.GenerateToken(users.Name);

            if(token == null)
            {
                return Unauthorized("Invalid attempt");
            }

            // saving refresh token to the db
            UserRefreshToken userRefreshToken = new UserRefreshToken
            {
                UserName = users.Name,
                Refresh_Token = token.Refresh_Token
            };

            _userServiceInterface.AddUserRefreshTokens(userRefreshToken);
            _userServiceInterface.SaveCommit();
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(Tokens tokens)
        {
            var principal = _JWTManager.GetPrincipalFromExpiredToken(tokens.Access_Token);
            var username = principal.Identity?.Name;

            // retreive the saved refresh token from database
            var savedRefreshToken = _userServiceInterface.GetSavedRefreshToken(username,tokens.Refresh_Token);

            if(savedRefreshToken.Refresh_Token != tokens.Refresh_Token)
            {
                    return Unauthorized("Invalid attempt");
            }

            var newJWTToken = _JWTManager.GenerateRefreshToken(username);
            if(newJWTToken == null)
            {
                return Unauthorized("Invalid attempt");
            }


            // saving refresh token to the db
            UserRefreshToken userRefreshToken = new UserRefreshToken
            {
                UserName = username,
                Refresh_Token = tokens.Refresh_Token
            };

            _userServiceInterface.DeleteUserRefreshToken(username, tokens.Refresh_Token);
            _userServiceInterface.AddUserRefreshTokens(userRefreshToken);
            _userServiceInterface.SaveCommit();

            return Ok(newJWTToken);
        }

    }
}

