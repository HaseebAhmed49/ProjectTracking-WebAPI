using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ProjectTracking_WebAPI.Data.Models
{
    public class Users:IdentityUser
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}

