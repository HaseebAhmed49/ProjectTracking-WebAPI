using System;
using System.Security.Claims;
using ProjectTracking_WebAPI.Data.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public interface IJWTManagerInterface
    {
        Tokens GenerateToken(string username);

        Tokens GenerateRefreshToken(string username);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

