using System;
using ProjectTracking_WebAPI.Data.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public interface IUserServiceInterface
    {
//        Task<bool> IsValidUserAsync(Users users);

        UserRefreshToken AddUserRefreshTokens(UserRefreshToken user);

        UserRefreshToken GetSavedRefreshToken(string username, string refreshtoken);

        void DeleteUserRefreshToken(string username, string refreshtoken);

        int SaveCommit();
    }
}

