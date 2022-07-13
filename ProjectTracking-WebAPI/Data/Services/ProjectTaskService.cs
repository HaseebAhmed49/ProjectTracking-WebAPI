using System;
using Microsoft.EntityFrameworkCore;
using ProjectTracking_WebAPI.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public class ProjectTaskService:IProjectTaskInterface
    {
        private readonly AppDBContext _context;

        public ProjectTaskService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<ProjectTask> AddProjectTask(ProjectTaskVM projectTaskVM)
        {
            var newProjectTask = new ProjectTask()
            {
                AssignedTo = projectTaskVM.AssignedTo,
                EmployeeID = projectTaskVM.EmployeeID,
                TaskEndDate = projectTaskVM.TaskEndDate,
                TaskCompletion = projectTaskVM.TaskCompletion,
                TaskStartDate = projectTaskVM.TaskStartDate,
                UserStoryID = projectTaskVM.UserStoryID
            };
            await _context.ProjectTask.AddAsync(newProjectTask);
            await _context.SaveChangesAsync();
            return newProjectTask;
        }

        public Task<ProjectTask> DeleteProjectTaskById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProjectTask>> GetAllProjectTasks() => _context.ProjectTask.ToListAsync();

        public Task<ProjectTask> GetProjectTaskById(int id)
        {
            var projectTask = _context.ProjectTask.FirstOrDefaultAsync(pt => pt.ProjectTaskID == id);
            return projectTask;
        }

        public Task<ProjectTask> UpdateProjectTaskById(int id, ProjectTaskVM projectVM)
        {
            throw new NotImplementedException();
        }
    }
}

