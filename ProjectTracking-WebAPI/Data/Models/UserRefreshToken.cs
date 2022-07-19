using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracking_WebAPI.Data.Models
{
    public class UserRefreshToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Refresh_Token { get; set; }

        public bool isActive { get; set; } = true;
    }
}

