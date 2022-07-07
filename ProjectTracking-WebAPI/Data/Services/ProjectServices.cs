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

        public Task<Project> AddProject(ProjectVM projectVM)
        {
            throw new NotImplementedException();
        }

        public Task<List<Project>> GetAllProjects() => _context.Project.ToListAsync();

        public Task<Project> GetProjectById(int id)
        {
            var project = _context.Project.FirstOrDefaultAsync(p => p.ProjectID == id);
            return project;
        }
    }
}

