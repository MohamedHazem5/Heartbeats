using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "الاسم مطلوب")]
        [StringLength(50, ErrorMessage = "الاسم يجب أن يكون بين {2} و {1} حرف", MinimumLength = 3)]
        public string Name { get; set; }
        public string Mobile { get; set; }

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صالح")]
        public string Email { get; set; }

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [StringLength(100, ErrorMessage = "كلمة المرور يجب أن تكون بين {2} و {1} حرف", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "كلمة المرور غير متطابقة")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }

    }
}
