using System;
using ProjectTracking_WebAPI.Data.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public interface IJWTManagerInterface
    {
        Tokens Auhtenticate(Users users);
    }
}

