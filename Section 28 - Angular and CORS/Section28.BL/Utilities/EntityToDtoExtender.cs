using Section27.DataAccess.Entities;
using Entities = Section27.DataAccess.Entities;

namespace Section27.BL.Utilities
{
    public static class EntityToDtoExtender
    {
        public static Entities.Master_User? DTOTOEntity(this DTO.User? dtoUser)
        {
            if (dtoUser is null) return null;

            Master_User user = new Master_User()
            {
                RecordID = dtoUser.RecordID,
                Name = dtoUser.Name,
                Email = dtoUser.Email,
                Country = dtoUser.Country,
                CreatedOn = dtoUser.CreatedOn,
                ModifiedOn = dtoUser.ModifiedOn
            };
            return user;
        }

        public static DTO.User? EntityTODTO (this Entities.Master_User? user)
        {
            if (user is null) return null;

            DTO.User dtoUser = new DTO.User()
            {
                RecordID = user.RecordID,
                Name = user.Name,
                Email = user.Email,
                Country = user.Country,
                CreatedOn = user.CreatedOn,
                ModifiedOn = user.ModifiedOn
            };
            return dtoUser;
        }

    }
}
