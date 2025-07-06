using System.ComponentModel.DataAnnotations;

namespace Section26.DataAccess.Entities
{
    public class Master_User
    {
        [Key]
        public Guid RecordID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public DateTime CreatedOn {  get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
