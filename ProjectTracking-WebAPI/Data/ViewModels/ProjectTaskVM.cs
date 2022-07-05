using System;
namespace ProjectTracking_WebAPI.Models
{
    public class ProjectTaskForEmployeeVM
    {
        public int ProjectTaskID { get; set; }

        public string AssignedTo { get; set; }

        public DateTime TaskStartDate { get; set; }

        public DateTime TaskEndDate { get; set; }

        public string TaskCompletion { get; set; }
    }
}

