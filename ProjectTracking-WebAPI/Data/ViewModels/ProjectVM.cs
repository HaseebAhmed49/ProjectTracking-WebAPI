using System;
namespace ProjectTracking_WebAPI.Models
{
    public class ProjectVM
    {
        public string ProjectName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ClientName { get; set; }
    }

    public class ProjectWithUserStoriesVM
    {
        public string ProjectName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ClientName { get; set; }

        public List<UserStoryForProjectVM> UserStories { get; set; }
    }
}

