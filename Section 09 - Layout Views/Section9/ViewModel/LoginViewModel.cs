using System.ComponentModel.DataAnnotations;

namespace Section9.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Invalid {0}")]
        [Display(Name = "User ID")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8,ErrorMessage ="{0} should be 8 - 20 character length")]
        public string Password { get; set; }
    }
}
