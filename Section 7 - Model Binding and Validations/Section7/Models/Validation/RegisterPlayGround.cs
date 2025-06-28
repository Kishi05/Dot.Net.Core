using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Section7.Models.Validation
{
    public class RegisterPlayGround
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3),MaxLength(20)]
        public string Name { get; set; }

        [StringLength(50)]
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
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public override string ToString()
        {
            return $"Id : {Id} \nName : {Name} \nAddress : {Address} \nCity : {City} \nRegion : {Region} \nPostalCode : {PostalCode} \nCountry : {Country} \nPhone : {Phone} \nEmail : {Email} \nPassword : {Password} \nConfirm Password : {ConfirmPassword}";
        }
    }
}
