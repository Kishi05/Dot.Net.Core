using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Services.Utilities;
using Services.DTO;
using Services.Users.Interface;
using Microsoft.Extensions.Logging;
using Serilog;
using SerilogTimings;

namespace Services.Users
{
    public class Users : IUsers
    {
        private NetCoreAppDBContext _dBContext;
        private ILogger<Users> _logger;
        private IDiagnosticContext _diagnosticContext;
        public Users(NetCoreAppDBContext dBContext, ILogger<Users> logger, IDiagnosticContext diagnosticContext)
        {
            _dBContext = dBContext;
            _logger = logger;
            _diagnosticContext = diagnosticContext;
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
            User? result = null;
            try
            {
                // Serilog Timing to capture code execution Turn Around Time
                // Cold Start - 1st EF query ~80 ms (model warm‑up + logging I/O). This timing can vary.
                using (Operation.Time("Timer To Fetch User Details"))
                {
                    result = _dBContext.Users.FirstOrDefault(x => x.Id == id)?.ToDTOEntity();
                }

                //Added Second time to check timings
                // Warm Path (steady‑state) - 	Subsequent queries ~8 ms (connection + model cached)
                using (Operation.Time("Timer To Fetch User Details - 2"))
                {
                    var s = _dBContext.Users.FirstOrDefault(x => x.Id == id)?.ToDTOEntity();
                }

                /* 
                 * -----------------------------------------------------------------
                 * Record the value as property inside Serilog
                 * If object is sent, it internally calls ToString() for each Object.
                 * Inorder to fetch proper details rather than DataType.
                 * We have created override method for ToString() inside User Class
                 * ----------------------------------------------------------------- 
                 */

                if (result != null)
                {
                    _diagnosticContext.Set("User", result);
                }

                return result;
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
