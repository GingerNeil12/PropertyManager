﻿using System;
using Microsoft.AspNetCore.Identity;

namespace PropertyManager.Infrastructure.Security.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
    }
}
