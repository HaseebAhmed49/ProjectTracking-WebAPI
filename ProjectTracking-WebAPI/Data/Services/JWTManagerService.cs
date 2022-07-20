using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProjectTracking_WebAPI.Data.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public class JWTManagerService: IJWTManagerInterface
    {
        private readonly IConfiguration _iconfiguration;
        public JWTManagerService(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        public Tokens GenerateToken(string username)
        {
            return GenerateJWTTokens(username);
        }

        public Tokens GenerateRefreshToken(string username)
        {
            return GenerateJWTTokens(username);
        }

        public Tokens GenerateJWTTokens(string username)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, username)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = GenerateRefreshToken();
                return new Tokens { Access_Token = tokenHandler.WriteToken(token), Refresh_Token = refreshToken };
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }


        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if(jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            return principal;
        }
    }
}

