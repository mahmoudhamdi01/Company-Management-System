using System.ComponentModel.DataAnnotations;

namespace PL.ViewModels
{
	public class LoginViewModel
	{
		[EmailAddress(ErrorMessage = "Invalid Email")]
		[Required(ErrorMessage = "Email Is Required")]
		public string Email { get; set; } = default!;
		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = default!;
        public bool RememberMe { get; set; }
    }
}
