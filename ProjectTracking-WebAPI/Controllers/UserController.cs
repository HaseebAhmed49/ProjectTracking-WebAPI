using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking_WebAPI.Data;
using ProjectTracking_WebAPI.Data.Models;
using ProjectTracking_WebAPI.Data.Services;
using ProjectTracking_WebAPI.Data.Static;
using ProjectTracking_WebAPI.Data.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectTracking_WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IJWTManagerInterface _JWTManager;
        private readonly IUserServiceInterface _userServiceInterface;

        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly AppDBContext _context;

        public UserController(IJWTManagerInterface jWTManager, UserManager<Users> userManager,SignInManager<Users> signInManager,AppDBContext context,IUserServiceInterface userServiceInterface)
        {
            _JWTManager = jWTManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
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
        public async Task<IActionResult> Authenticate([FromBody] UserVM users)
        {
            var validUser = await _userManager.FindByEmailAsync(users.Email);
            if (validUser == null) return Unauthorized("Incorrect username or password");
            if(validUser != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(validUser,users.Password);
                if(passwordCheck)
                {
                    var token = _JWTManager.GenerateToken(users.Name);
                    if (token == null)
                        return Unauthorized("Invalid attempt");
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
            }
            return Unauthorized("Incorrect username or password");
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
                    return Unauthorized("Invalid attempt");

            var newJWTToken = _JWTManager.GenerateRefreshToken(username);
            if(newJWTToken == null)
                return Unauthorized("Invalid attempt");
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

        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<IActionResult> Register(UserVM userVM)
        {
            if (!ModelState.IsValid) return BadRequest("Check User Details again!");
            var user = await _userManager.FindByEmailAsync(userVM.Email);
            if (user != null)
            {
                return BadRequest($"User with this email {userVM.Email} already exists");
            }

            var newAdminUser = new Users()
            {
                Name = userVM.Name,
                UserName = userVM.Name,
                Email = userVM.Email,
                EmailConfirmed = true,
            };
            await _userManager.CreateAsync(newAdminUser, userVM.Password);
            await _userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
            return Ok(newAdminUser);
        }
    }
}

