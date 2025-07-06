using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Section25.ViewModels
{
    public class UserViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        //[EmailAddress]
        [Remote(action: "IsEMailRegistered", controller: "Home", ErrorMessage ="Email ID already in use")]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        public AdminEnum Role { get; set; }
    }
}
