﻿using System.ComponentModel.DataAnnotations;

namespace ParadiseCommerce.Services.Users.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}