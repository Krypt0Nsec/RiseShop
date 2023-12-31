﻿using Microsoft.AspNetCore.Identity;

namespace Rise.Services.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string LastName { get; set; }

        public string? AboutMe { get; set; }
    }
}
