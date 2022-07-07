using System;
using ProjectTracking_WebAPI.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public interface IProjectInterface
    {
        public Task<List<Project>> GetAllProjects();

        public Task<Project> GetProjectById(int id);

        public Task<Project> AddProject(ProjectVM projectVM);

        public Task<Project> UpdateProjectById(int id,ProjectVM projectVM);

        public Task<Project> DeleteProjectById(int id);

        public Task<ProjectWithUserStoriesVM> GetProjectWithUserStoriesById(int id);
    }
}