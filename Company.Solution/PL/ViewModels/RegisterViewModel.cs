using System.ComponentModel.DataAnnotations;

namespace PL.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "FirstName Is Required")]
        public string FirstName { get; set; } = default!;
        [Required(ErrorMessage = "LastName Is Required")]
        public string LastName { get; set; } = default!;
        [EmailAddress(ErrorMessage ="Invalid Email")]
        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare("Password", ErrorMessage ="Password Dosen't Match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = default!;
        public bool IsAgree { get; set; }
    }
}
