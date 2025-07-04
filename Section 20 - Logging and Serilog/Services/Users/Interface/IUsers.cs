using Services.DTO;

namespace Services.Users.Interface
{
    public interface IUsers
    {
        List<User>? GetUsers();
        User? GetUserByID(int? id);
        User? AddEditUser(User? user);
        bool DeleteUser(int? id);
    }
}
