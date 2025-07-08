using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Section29.Core.DTO
{
    public class RegisterDTO
    {
        [Required]

        public string PersonName {  get; set; }

        [Remote(action: "IsEmailAvailable",controller:"Home")]
        [EmailAddress]
        [Required]
        public string Email {  get; set; }

        [Required]
        [RegularExpression("^[0-9]*$")]
        public string Phone {  get; set; }

        [Required]
        public string Password {  get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
