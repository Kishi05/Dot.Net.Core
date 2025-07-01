using Services.DTO;
using Services.Users.Interface;
using System.Xml.Linq;

namespace Services.Users
{
    public class Users : IUsers
    {
        private List<User> userList { get; set; }
        public Users()
        {
            userList = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Name = "Test User",
                    Location = "UK",
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow,
                    Email = "testuser@netcore.com",
                    isDummy = true
                }
            };
        }
        public List<User> GetUsers()
        {
            return userList;
        }
        public User? GetUserByID(int? id)
        {
            if(!id.HasValue)
                return null;
            return userList.Where(x => x.Id == id).FirstOrDefault();
        }
        public User? AddEditUser(User? user)
        {
            if(user != null)
            {
                User? dbUser = userList.Where(x => x.Id == user.Id).FirstOrDefault();
                if (dbUser != null)
                {
                    User newUser = new User();
                    newUser.Id = dbUser.Id;
                    newUser.Name = user.Name;
                    newUser.Location = user.Location;
                    newUser.CreatedOn = dbUser.CreatedOn;
                    newUser.ModifiedOn = DateTime.UtcNow;
                    newUser.Email = user.Email;
                    newUser.isDummy = dbUser.isDummy;

                    userList.Remove(dbUser);
                    userList.Add(newUser);
                    
                }
                else
                {
                    user.Id = userList.Count()+1;
                    user.CreatedOn = DateTime.UtcNow;
                    user.ModifiedOn = DateTime.UtcNow;
                    userList.Add(user);
                }
            }

            return user;
        }

        public bool DeleteUser(int? id)
        {
            bool result = false;
            if (id != null)
            {
                var user = userList.Where(x => x.Id == id).FirstOrDefault();
                if (user != null)
                {
                    userList.Remove(user);
                    result = true;
                }
            }
            return result;
        }
    }
}
