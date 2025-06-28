using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Section7.Models.Validation
{
    public class RegisterPlayGround
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is Mandatory")]
        //[ValidateNever] -> Skip Validation, it skips even if other annotations are present and can place it any order.
        [Display(Name = "Person Name")]
        [MinLength(3), MaxLength(20)]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 10, ErrorMessage = "{0} should be Mininum {2} to Maximum {1} characters")]
        [MaxLength(50)]
        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Region { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Only numeric digits are allowed.")]
        [MaxLength(10)]
        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "{1} and {0} doesnot match")]
        public string ConfirmPassword { get; set; }

        public override string ToString()
        {
            return $"Id : {Id} \nName : {Name} \nAddress : {Address} \nCity : {City} \nRegion : {Region} \nPostalCode : {PostalCode} \nCountry : {Country} \nPhone : {Phone} \nEmail : {Email} \nPassword : {Password} \nConfirm Password : {ConfirmPassword}";
        }
    }
}
