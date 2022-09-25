using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public async Task<ProjectTask> DeleteProjectTaskById(int id)
        {
            var projectTask = await _context.ProjectTask.FirstOrDefaultAsync(pt => pt.ProjectTaskID == id);
            if (projectTask != null)
            {
                _context.ProjectTask.Remove(projectTask);
                await _context.SaveChangesAsync();
                return projectTask;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ProjectTask>> GetAllProjectTasks() => await _context.ProjectTask.ToListAsync();

        public async Task<List<ProjectTaskForEmployeeVM>> GetAllProjectTasksForAnEmployee(int employeeId)
        {
            var allProjectTasksForAnEmployee = await _context.ProjectTask.Where(id => id.EmployeeID == employeeId).Select(pt => new ProjectTaskForEmployeeVM()
            {
                AssignedTo = pt.AssignedTo,
                ProjectTaskID = pt.ProjectTaskID,
                TaskCompletion = pt.TaskCompletion,
                TaskEndDate = pt.TaskEndDate,
                TaskStartDate = pt.TaskStartDate
            }).ToListAsync();
            return allProjectTasksForAnEmployee;
        }

        public async Task<List<ProjectTaskWithDetailsVM>> GetAllProjectTasksWithDetails()
        {
            var allProjectTasksWithDetails = new List<ProjectTaskWithDetailsVM>();
            var allProjectTask = await _context.ProjectTask.ToListAsync();
            if(allProjectTask.Count>0)
            {
                foreach(var item in allProjectTask)
                {
                    var projectTaskWithDetails = _context.ProjectTask.Where(pt => pt.ProjectTaskID == item.ProjectTaskID).Select(ptd => new ProjectTaskWithDetailsVM()
                    {
                        AssignedTo = ptd.AssignedTo,
                        TaskCompletion = ptd.TaskCompletion,
                        TaskEndDate = ptd.TaskEndDate,
                        TaskStartDate = ptd.TaskStartDate,
                        Employee = _context.Employee.FirstOrDefault(emp => emp.EmployeeID == ptd.EmployeeID),
                        UserStory = _context.UserStory.FirstOrDefault(us => us.UserStoryID == ptd.UserStoryID)
                    }).FirstOrDefault();
                    allProjectTasksWithDetails.Add(projectTaskWithDetails);
                }
            }
            return allProjectTasksWithDetails;
        }

        public async Task<ProjectTask> GetProjectTaskById(int id)
        {
            var projectTask = await _context.ProjectTask.FirstOrDefaultAsync(pt => pt.ProjectTaskID == id);
            return projectTask;
        }

        public async Task<ProjectTaskWithDetailsVM> GetProjectTaskWithDetails(int id)
        {
            var projectTaskWithDetails = _context.ProjectTask.Where(pt => pt.ProjectTaskID == id).Select(ptd => new ProjectTaskWithDetailsVM()
            {
                AssignedTo = ptd.AssignedTo,
                TaskCompletion = ptd.TaskCompletion,
                TaskEndDate = ptd.TaskEndDate,
                TaskStartDate = ptd.TaskStartDate,
                Employee = _context.Employee.FirstOrDefault(emp => emp.EmployeeID == ptd.EmployeeID),
                UserStory = _context.UserStory.FirstOrDefault(us => us.UserStoryID == ptd.UserStoryID)
            }).FirstOrDefault();
            return projectTaskWithDetails;
        }

        public async Task<ProjectTask> UpdateProjectTaskById(int id, ProjectTaskVM projectTaskVM)
        {
            var projectTask = await _context.ProjectTask.FirstOrDefaultAsync(pt => pt.ProjectTaskID == id);
            if(projectTask!=null)
            {
                projectTask.AssignedTo = projectTaskVM.AssignedTo;
                projectTask.EmployeeID = projectTaskVM.EmployeeID;
                projectTask.TaskEndDate = projectTaskVM.TaskEndDate;
                projectTask.TaskCompletion = projectTaskVM.TaskCompletion;
                projectTask.TaskStartDate = projectTaskVM.TaskStartDate;
                projectTask.UserStoryID = projectTaskVM.UserStoryID;
                _context.ProjectTask.Update(projectTask);
                await _context.SaveChangesAsync();
                return projectTask;
            }
            else
            {
                return null;
            }
        }

    }
}

