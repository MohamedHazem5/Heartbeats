using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UsersRole { get; set; }
    }
}
