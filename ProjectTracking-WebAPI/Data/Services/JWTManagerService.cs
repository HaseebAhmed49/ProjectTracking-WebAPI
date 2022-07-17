using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProjectTracking_WebAPI.Data.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public class JWTManagerService: IJWTManagerInterface
    {

        Dictionary<string, string> UserRecords = new Dictionary<string, string>
        {
            { "user1","password1"},
            { "user2","password2"},
            { "user3","password3"}
        };


        private readonly IConfiguration _iconfiguration;
        public JWTManagerService(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        public Tokens Auhtenticate(Users users)
        {
            if(!UserRecords.Any(x => x.Key == users.Name && x.Value == users.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, users.Name)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}

