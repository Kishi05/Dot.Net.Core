using Section20.ViewModels;
using Services.DTO;

namespace Section20.Utilities
{
    public static class MapHelper
    {
        public static UserViewModel ToViewModel(this User user)
        {
            if (user is null) return null!;

            return new UserViewModel
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
        public static User ToEntity(this UserViewModel user)
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
