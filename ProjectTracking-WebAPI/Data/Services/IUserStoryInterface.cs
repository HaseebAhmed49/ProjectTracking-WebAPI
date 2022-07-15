using System;
using ProjectTracking_WebAPI.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public interface IUserStoryInterface
    {
        public Task<List<UserStory>> GetAllUserStories();

        public Task<UserStory> GetUserStoryById(int id);

        public Task<UserStory> AddUserStory(UserStoryVM userStoryVM);

        public Task<UserStory> UpdateUserStoryById(int id, UserStoryVM userStoryVM);

        public Task<UserStory> DeleteUserStoryById(int id);
    }
}