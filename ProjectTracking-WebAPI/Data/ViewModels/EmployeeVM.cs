using System;
namespace ProjectTracking_WebAPI.Models
{
    public class EmployeeVM
    {
        public string EmployeeName { get; set; }

        public string Designation { get; set; }

        public string ContactNo { get; set; }

        public string EmailID { get; set; }

        public string SkillSets { get; set; }
    }


    public class EmployeeWithProjectTasks
    {
        public string EmployeeName { get; set; }

        public string Designation { get; set; }

        public string ContactNo { get; set; }

        public string EmailID { get; set; }

        public string SkillSets { get; set; }

        public List<ProjectTaskForEmployeeVM> ProjectTasks { get; set; }
    }
}

