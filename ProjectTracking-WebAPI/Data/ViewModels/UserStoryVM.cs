using System;
namespace ProjectTracking_WebAPI.Models
{
    public class UserStoryForProjectVM
    {
        public string Story { get; set; }
    }

    public class UserStoryVM
    {
        public string Story { get; set; }

        public int ProjectID { get; set; }
    }

    public class UserStoryWithDetailsVM
    {
        public string Story { get; set; }

        public Project Project { get; set; }

        public List<ProjectTask>? ProjectTasks { get; set; }
    }
}

