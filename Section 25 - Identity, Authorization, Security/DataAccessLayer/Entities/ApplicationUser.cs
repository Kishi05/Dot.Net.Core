using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? Name { get; set; }
    }
}
