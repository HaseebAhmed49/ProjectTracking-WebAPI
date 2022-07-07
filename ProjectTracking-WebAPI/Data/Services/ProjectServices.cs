using System;
using Microsoft.EntityFrameworkCore;
using ProjectTracking_WebAPI.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public class ProjectServices: IProjectInterface
    {
        private readonly AppDBContext _context;

        public ProjectServices(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Project> AddProject(ProjectVM projectVM)
        {
            var newProject = new Project()
            {
                ClientName = projectVM.ClientName,
                StartDate = projectVM.StartDate,
                EndDate = projectVM.EndDate,
                ProjectName = projectVM.ProjectName,
            };
            await _context.Project.AddAsync(newProject);
            await _context.SaveChangesAsync();
            return newProject;
        }

        public async Task<Project> UpdateProjectById(int id,ProjectVM projectVM)
        {
            var found = await _context.Project.FirstOrDefaultAsync(p => p.ProjectID == id);
            if(found!=null)
            {
                found.ClientName = projectVM.ClientName;
                found.StartDate = projectVM.StartDate;
                found.EndDate = projectVM.EndDate;
                found.ProjectName = projectVM.ProjectName;
                _context.Update(found);
                await _context.SaveChangesAsync();
            };
            return found;
        }

        public async Task<Project> DeleteProjectById(int id)
        {
            var found = await _context.Project.FirstOrDefaultAsync(p => p.ProjectID == id);
            if (found != null)
            {
                _context.Project.Remove(found);
                await _context.SaveChangesAsync();
            };
            return found;
        }



        public Task<List<Project>> GetAllProjects() => _context.Project.ToListAsync();

        public Task<Project> GetProjectById(int id)
        {
            var project = _context.Project.FirstOrDefaultAsync(p => p.ProjectID == id);
            return project;
        }

        public async Task<ProjectWithUserStoriesVM> GetProjectWithUserStoriesById(int id)
        {
            var project = _context.Project.Where(p => p.ProjectID == id).Select(pus => new ProjectWithUserStoriesVM()
            {
                ClientName = pus.ClientName,
                StartDate = pus.StartDate,
                EndDate = pus.EndDate,
                ProjectName = pus.ProjectName,
                UserStories = pus.UserStories.Where(p => p.ProjectID == id).Select(us => new UserStoryForProjectVM()
                {
                    Story = us.Story
                }).ToList()
            }).FirstOrDefault();
            return project;
        }
    }
}

