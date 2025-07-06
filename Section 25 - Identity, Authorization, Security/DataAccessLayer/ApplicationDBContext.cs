using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ApplicationDBContext :IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }
    }
}
