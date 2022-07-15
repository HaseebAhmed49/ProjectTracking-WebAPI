using System;
using Microsoft.EntityFrameworkCore;
using ProjectTracking_WebAPI.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public class UserStoryService:IUserStoryInterface
    {
        private AppDBContext _context;
        public UserStoryService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<UserStory> AddUserStory(UserStoryVM userStoryVM)
        {
            var newUserStory = new UserStory()
            {
                ProjectID = userStoryVM.ProjectID,
                Story = userStoryVM.Story,
            };
            await _context.UserStory.AddAsync(newUserStory);
            await _context.SaveChangesAsync();
            return newUserStory;
        }

        public async Task<UserStory> DeleteUserStoryById(int id)
        {
            var userStory = await _context.UserStory.FirstOrDefaultAsync(us => us.UserStoryID == id);
            if(userStory!=null)
            {
                _context.UserStory.Remove(userStory);
                await _context.SaveChangesAsync();
            }
            return userStory;
        }

        public Task<List<UserStory>> GetAllUserStories() => _context.UserStory.ToListAsync();

        public Task<UserStory> GetUserStoryById(int id)
        {
            var userStory = _context.UserStory.FirstOrDefaultAsync(us => us.UserStoryID == id);
            return userStory;
        }

        public async Task<UserStoryWithDetailsVM> GetUserStoryWithProjectDetails(int id)
        {
            var userStoryWithDetails = _context.UserStory.Where(us => us.UserStoryID == id).Select(usd => new UserStoryWithDetailsVM()
            {
                Story = usd.Story,
                ProjectTasks = usd.ProjectTasks.Where(pt => pt.UserStoryID == id).ToList(),
                Project = _context.Project.FirstOrDefault(p => p.ProjectID == usd.ProjectID)                
            }).FirstOrDefault();
            return userStoryWithDetails;
        }

        public async Task<UserStory> UpdateUserStoryById(int id, UserStoryVM userStoryVM)
        {
            var userStoryFound = await _context.UserStory.FirstOrDefaultAsync(us => us.UserStoryID == id);
            if (userStoryFound != null)
            {
                userStoryFound.ProjectID = userStoryVM.ProjectID;
                userStoryFound.Story = userStoryVM.Story;
                _context.Update(userStoryFound);
                await _context.SaveChangesAsync();
            };
            return userStoryFound;
        }
    }
}

