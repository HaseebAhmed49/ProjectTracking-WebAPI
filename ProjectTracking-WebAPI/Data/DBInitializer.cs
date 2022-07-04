using System;
using ProjectTracking_WebAPI.Models;

namespace ProjectTracking_WebAPI.Data
{
    public class DBInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDBContext>();
                if (!context.Employee.Any())
                {
                    context.Employee.AddRange(new Employee()
                    {
                        EmployeeName="Haseeb Ahmed",
                        EmailID="haseeb.ahmed@powersoft19.com",
                        Designation = "Software Developer",
                        ContactNo = "03338437949",
                        SkillSets = "dotNet Framework, MS Office, Automated Testing"                      
                    },
                    new Employee()
                    {
                        EmployeeName = "Muhammad Farhan Ali",
                        EmailID = "farhan.ali@powersoft19.com",
                        Designation = "Software Developer",
                        ContactNo = "03338437949",
                        SkillSets = "Automated Testing, MS Office"
                    },
                    new Employee()
                    {
                        EmployeeName = "Ibrar Sakhi Khan",
                        EmailID = "ibrar.sakhi@powersoft19.com",
                        Designation = "Software Developer",
                        ContactNo = "03338437949",
                        SkillSets = "dotNet Framework, MS Office"
                    });
                    context.SaveChanges();
                }

                if(!context.Project.Any())
                {
                    context.Project.AddRange(new Project()
                    {
                        ClientName = "SmartWires",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(3650),
                        ProjectName = "SmartValve V1.04",
                    },
                    new Project()
                    {
                        ClientName = "Engineering System Concern Private Ltd",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(365),
                        ProjectName = "Automated Gate Sliding",
                    });
                    context.SaveChanges();
                }

                if(!context.UserStory.Any())
                {
                    context.UserStory.AddRange(new UserStory()
                    {
                        ProjectID = 1,
                        Story = "First Story"
                    },
                    new UserStory()
                    {
                        ProjectID = 2,
                        Story = "Second Story"
                    });
                    context.SaveChanges();

                }

                if(!context.ProjectTask.Any())
                {
                    context.ProjectTask.AddRange(new ProjectTask()
                    {
                        AssignedTo = "Haseeb Ahmed",
                        TaskStartDate = DateTime.Now.AddDays(10),
                        TaskEndDate = DateTime.Now.AddDays(22),
                        EmployeeID = 1,
                        UserStoryID = 1,
                        TaskCompletion = "Not Started",                        
                    },
                    new ProjectTask()
                    {
                        AssignedTo = "Muhammad Farhan Ali",
                        TaskStartDate = DateTime.Now.AddDays(5),
                        TaskEndDate = DateTime.Now.AddDays(12),
                        EmployeeID = 2,
                        UserStoryID = 2,
                        TaskCompletion = "In Progress",
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}

