using System;
namespace ProjectTracking_WebAPI.Models
{
    public class UserStory
    {
        public int UserStoryID { get; set; }

        public string Story { get; set; }

        public int ProjectID { get; set; }

        public Project Project { get; set; }

        public List<ProjectTask>? ProjectTasks { get; set; }
    }
}

