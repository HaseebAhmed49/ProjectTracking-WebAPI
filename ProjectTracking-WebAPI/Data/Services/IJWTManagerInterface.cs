using System;
using System.Security.Claims;
using ProjectTracking_WebAPI.Data.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public interface IJWTManagerInterface
    {
        Tokens Auhtenticate(Users users);

        Tokens GenerateRefreshToken(Users users);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

