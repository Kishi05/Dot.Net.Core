using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users.Interface
{
    public interface IUsers
    {
        List<User> GetUsers();
        User? GetUserByID(int? id);
        User? AddEditUser(User? user);
        bool DeleteUser(int? id);
    }
}
