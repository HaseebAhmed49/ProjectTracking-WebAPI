using System;
namespace ProjectTracking_WebAPI.Data.ViewModels
{
    public class UserVM
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class SignUpVM
    {
        public string Name { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}