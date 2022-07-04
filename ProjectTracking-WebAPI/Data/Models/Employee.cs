using System;
namespace ProjectTracking_WebAPI.Models
{
    public class Employee
    {
        public int EmoloyeeID { get; set; }

        public string EmployeeName { get; set; }

        public string Designation { get; set; }

        public string ContactNo { get; set; }

        public string EmailID { get; set; }

        public string SkillSets { get; set; }

        public List<ProjectTask>? ProjectTasks { get; set; }
    }
}

