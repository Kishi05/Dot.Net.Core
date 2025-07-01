using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Section18.Utilities;
using Services.DTO;
using Services.Users.Interface;

namespace Services.Users
{
    public class Users : IUsers
    {
        private NetCoreAppDBContext _dBContext;
        public Users(NetCoreAppDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public List<User> GetUsers()
        {
            return _dBContext.Users
                .AsNoTracking()
                .Select(x => x.ToDTOEntity())
                .ToList();
        }
        public User? GetUserByID(int? id)
        {

            if (!id.HasValue) return null;

            return _dBContext.Users.FirstOrDefault(x => x.Id == id)?.ToDTOEntity();

        }
        public User? AddEditUser(User? dtoUser)
        {
            if (dtoUser is null) return null;

            Admin_User? dbUser = _dBContext.Users.FirstOrDefault(x => x.Id == dtoUser.Id);

            if (dbUser is not null)
            {
                dbUser.Name = dtoUser.Name;
                dbUser.Email = dtoUser.Email;
                dbUser.Location = dtoUser.Location;
                dbUser.ModifiedOn = DateTime.UtcNow;
            }
            else
            {
                var newUser = dtoUser.ToDBEntity();
                newUser.CreatedOn = DateTime.UtcNow;
                newUser.ModifiedOn = DateTime.UtcNow;

                _dBContext.Users.Add(newUser);

                dbUser = newUser;
            }

            _dBContext.SaveChanges();

            return dbUser.ToDTOEntity();
        }

        public bool DeleteUser(int? id)
        {
            if (id is null) return false;

            var user = _dBContext.Users.FirstOrDefault(x => x.Id == id);
            if (user is null || user.isDummy) return false;

            _dBContext.Users.Remove(user);
            _dBContext.SaveChanges();

            return true;
        }
    }
}
