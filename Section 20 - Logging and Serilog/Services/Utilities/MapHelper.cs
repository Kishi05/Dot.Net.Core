using DataAccessLayer.Models;
using Services.DTO;

namespace Services.Utilities
{
    public static class MapHelper
    {
        public static Admin_User ToDBEntity(this User user)
        {
            if (user is null) return null!;

            return new Admin_User
            {
                Id = user.Id,
                Name = user.Name,
                Location = user.Location,
                Email = user.Email,
                CreatedOn = user.CreatedOn,
                ModifiedOn = user.ModifiedOn,
                isDummy = user.isDummy
            };
        }
        public static User ToDTOEntity(this Admin_User user)
        {
            if (user is null) return null!;

            return new User
            {
                Id = user.Id,
                Name = user.Name,
                Location = user.Location,
                Email = user.Email,
                CreatedOn = user.CreatedOn,
                ModifiedOn = user.ModifiedOn,
                isDummy = user.isDummy
            };
        }
    }
}
