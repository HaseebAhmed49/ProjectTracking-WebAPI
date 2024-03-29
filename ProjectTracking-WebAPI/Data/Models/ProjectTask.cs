﻿using System;
namespace ProjectTracking_WebAPI.Models
{
    public class ProjectTask
    {
        public int ProjectTaskID { get; set; }

        public string AssignedTo { get; set; }

        public DateTime TaskStartDate { get; set; }

        public DateTime TaskEndDate { get; set; }

        public string TaskCompletion { get; set; }

        public int EmployeeID { get; set; }

        public Employee Employee { get; set; }

        public int UserStoryID { get; set; }

        public UserStory UserStory { get; set; }
    }
}

