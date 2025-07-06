using Section26.DTO;

namespace Section26.BL.Interface
{
    public interface IUserCoordinator
    {
        Task<List<User?>> GetUsers();
        Task<User?> GetUserByID(Guid recordID);
        Task<User?> CreateUser(User newUser);
        Task<User?> UpdateUser(Guid recordID, User updateUser);
        Task<bool> DeleteUser(Guid recordID);
    }
}
