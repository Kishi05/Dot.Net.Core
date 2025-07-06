using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Section27.BL.Interface;
using Section27.BL.Utilities;
using Section27.DataAccess;
using Section27.DataAccess.Entities;
using Section27.DTO;
using Entities = Section27.DataAccess.Entities;

namespace Section27.BL
{
    public class UserCoordinator : IUserCoordinator
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ILogger<UserCoordinator> _logger;
        public UserCoordinator(ApplicationDBContext dbContext, ILogger<UserCoordinator> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<List<User?>> GetUsers()
        {
            return await _dbContext.Users.AsNoTracking().Select(x => x.EntityTODTO()).ToListAsync();
        }

        public async Task<User?> GetUserByID(Guid recordID)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .Where(x => x.RecordID == recordID)
                .Select(x => x.EntityTODTO())
                .FirstOrDefaultAsync();
        }

        public async Task<User?> CreateUser(User newUser)
        {
            if(newUser is null) return null;

            try
            {

                Master_User? user = newUser.DTOTOEntity();
                user.CreatedOn = DateTime.UtcNow;
                user.ModifiedOn = DateTime.UtcNow;

                await _dbContext.Users.AddAsync(user);

                await _dbContext.SaveChangesAsync();

                return user.EntityTODTO();
            }
            catch(Exception ex)
            {
                _logger.LogError("{ClassName}.{MethodName} - Exception - {ExceptionMessage}", nameof(UserCoordinator), nameof(CreateUser), ex.Message);
            }

            return null;
        }

        public async Task<User?> UpdateUser(Guid recordID, User updateUser)
        {
            if(updateUser is null) return null;

            try
            {

                Master_User? user = await _dbContext.Users.FindAsync(recordID);

                if (user is null) return null;

                user.Name = updateUser.Name;
                user.Email = updateUser.Email;
                user.Country = updateUser.Country;
                user.ModifiedOn = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();

                return user.EntityTODTO();

            }
            catch(Exception ex)
            {
                _logger.LogError("{ClassName}.{MethodName} - Exception - {ExceptionMessage}", nameof(UserCoordinator), nameof(UpdateUser), ex.Message);
            }

            return null;
        }

        public async Task<bool> DeleteUser(Guid recordID)
        {
            try
            {
                Master_User? user = await _dbContext.Users.FindAsync(recordID);

                if (user is null) return false;

                _dbContext.Users.Remove(user);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("{ClassName}.{MethodName} - Exception - {ExceptionMessage}", nameof(UserCoordinator), nameof(DeleteUser), ex.Message);
            }

            return false;
        }

    }
}
