using System;
using Microsoft.AspNetCore.Identity;
using ProjectTracking_WebAPI.Data.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public class UserService: IUserServiceInterface
    {
//        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDBContext _context;

        public UserService(AppDBContext context)
        {
            _context = context;
        }

        public UserRefreshToken AddUserRefreshTokens(UserRefreshToken user)
        {
            _context.UserRefreshTokens.Add(user);
            return user;
        }

        public void DeleteUserRefreshToken(string username, string refreshtoken)
        {
            var item = _context.UserRefreshTokens.FirstOrDefault(x => x.UserName == username && x.Refresh_Token == refreshtoken);
            if(item!=null)
            {
                _context.UserRefreshTokens.Remove(item);
            }
        }

        public UserRefreshToken GetSavedRefreshToken(string username, string refreshtoken) => _context.UserRefreshTokens.FirstOrDefault(x => x.UserName == username && x.Refresh_Token == refreshtoken && x.isActive == true);

        public int SaveCommit()
        {
            return _context.SaveChanges();
        }

        //public async Task<bool> IsValidUserAsync(Users users)
        //{
        //    var u = _userManager.Users.FirstOrDefault(o => o.UserName == users.Name);
        //    var result = await _userManager.CheckPasswordAsync(u, users.PasswordHash);
        //    return result;
        //}

    }
}

