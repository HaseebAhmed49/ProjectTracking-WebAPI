using System;
namespace ProjectTracking_WebAPI.Models
{
    public class Project
    {
        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ClientName { get; set; }

        public List<UserStory> UserStories { get; set; }
    }
}

