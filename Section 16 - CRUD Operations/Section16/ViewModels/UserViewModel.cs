using System.ComponentModel.DataAnnotations;

namespace Section16.ViewModels
{
    public class UserViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime CreatedOn {  get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool isDummy {  get; set; }
    }
}
