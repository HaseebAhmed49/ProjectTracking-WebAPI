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
}

