using System.ComponentModel.DataAnnotations;

namespace Section7.Models
{
    public class RegisterCommon
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }        
        public string Email { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"Id : {Id}\nName : {Name}\nEmail : {Email}\nPhone : {Phone}";
        }
    }
}
