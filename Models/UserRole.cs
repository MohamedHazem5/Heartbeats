﻿using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
