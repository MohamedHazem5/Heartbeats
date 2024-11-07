using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string ImgaeUrl { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
