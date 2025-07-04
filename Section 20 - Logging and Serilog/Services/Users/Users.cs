using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Services.Utilities;
using Services.DTO;
using Services.Users.Interface;
using Microsoft.Extensions.Logging;

namespace Services.Users
{
    public class Users : IUsers
    {
        private NetCoreAppDBContext _dBContext;
        private ILogger<Users> _logger;
        public Users(NetCoreAppDBContext dBContext, ILogger<Users> logger)
        {
            _dBContext = dBContext;
            _logger = logger;
        }
        public List<User>? GetUsers()
        {
            try
            {
                return _dBContext.Users
                    .AsNoTracking()
                    .Select(x => x.ToDTOEntity())
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message : {ex.Message}");
            }
            return null;
        }
        public User? GetUserByID(int? id)
        {

            if (!id.HasValue) return null;

            try
            {
                return _dBContext.Users.FirstOrDefault(x => x.Id == id)?.ToDTOEntity();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message : {ex.Message}");
            }

            return null;

        }
        public User? AddEditUser(User? dtoUser)
        {
            if (dtoUser is null) return null;

            try
            {

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
            catch (Exception ex)
            {
                _logger.LogError($"Error Message : {ex.Message}");
            }
            return null;
        }

        public bool DeleteUser(int? id)
        {
            if (id is null) return false;

            try
            {
                var user = _dBContext.Users.FirstOrDefault(x => x.Id == id);
                if (user is null || user.isDummy) return false;

                _dBContext.Users.Remove(user);
                _dBContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message : {ex.Message}");
                return false;
            }
        }
    }
}
