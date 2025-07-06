using Microsoft.AspNetCore.Mvc;
using Section26.BL.Interface;

namespace Section26.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserCoordinator _userCoordinator;

        public UserController(IUserCoordinator userCoordinator)
        {
            _userCoordinator = userCoordinator;
        }

        [Route("")]
        [HttpGet]
        public async Task<List<DTO.User?>> GetUsers()
        {
            return await _userCoordinator.GetUsers();
        }

        [Route("{userID}")]
        [HttpGet]
        public async Task<DTO.User?> GetUsersByID(Guid userID)
        {
            return await _userCoordinator.GetUserByID(userID);
        }

        [HttpPost]
        public async Task<DTO.User?> CreateUsers([FromBody]DTO.User newUser)
        {
            return await _userCoordinator.CreateUser(newUser);
        }

        [Route("{userID}")]
        [HttpPut]
        public async Task<DTO.User?> UpdateUsers(Guid userID, [FromBody] DTO.User updateUser)
        {
            return await _userCoordinator.UpdateUser(userID, updateUser);
        }

        [Route("{userID}")]
        [HttpDelete]
        public async Task<bool> DeleteUser(Guid userID)
        {
            return await _userCoordinator.DeleteUser(userID);
        }

    }
}
