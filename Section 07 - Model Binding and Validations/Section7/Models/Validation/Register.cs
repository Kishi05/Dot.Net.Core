using System.ComponentModel.DataAnnotations;

namespace Section7.Models.Validation
{
    public class Register
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

        public override string ToString()
        {
            return $"Id : {Id} \nName : {Name} \nAddress : {Address} \nCity : {City} \nRegion : {Region} \nPostalCode : {PostalCode} \nCountry : {Country} \nPhone : {Phone}";
        }
    }
}
