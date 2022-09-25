using System;
using ProjectTracking_WebAPI.Models;

namespace ProjectTracking_WebAPI.Data.Services
{
    public interface IProjectTaskInterface
    {
        public Task<List<ProjectTask>> GetAllProjectTasks();

        public Task<ProjectTaskWithDetailsVM> GetProjectTaskWithDetails(int id);

        public Task<List<ProjectTaskForEmployeeVM>> GetAllProjectTasksForAnEmployee(int employeeId);

        public Task<List<ProjectTaskWithDetailsVM>> GetAllProjectTasksWithDetails();

        public Task<ProjectTask> GetProjectTaskById(int id);

        public Task<ProjectTask> AddProjectTask(ProjectTaskVM projectVM);

        public Task<ProjectTask> UpdateProjectTaskById(int id,ProjectTaskVM projectVM);

        public Task<ProjectTask> DeleteProjectTaskById(int id);
    }
}