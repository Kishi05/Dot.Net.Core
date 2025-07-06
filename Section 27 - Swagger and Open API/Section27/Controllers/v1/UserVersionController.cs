using Microsoft.AspNetCore.Mvc;
using Section27.BL.Interface;

namespace Section27.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserVersionController : ControllerBase
    {
        private readonly IUserCoordinator _userCoordinator;

        public UserVersionController(IUserCoordinator userCoordinator)
        {
            _userCoordinator = userCoordinator;
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns>List DTO.User?</returns>
        [Route("")]
        [HttpGet]
        public async Task<List<DTO.User?>> GetUsers()
        {
            return await _userCoordinator.GetUsers();
        }

        /// <summary>
        /// Get User based on ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>DTO.User?</returns>
        [Route("{userID}")]
        [HttpGet]
        public async Task<DTO.User?> GetUsersByID(Guid userID)
        {
            return await _userCoordinator.GetUserByID(userID);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>DTO.User?</returns>
        [HttpPost]
        public async Task<DTO.User?> CreateUsers([FromBody]DTO.User newUser)
        {
            return await _userCoordinator.CreateUser(newUser);
        }

        /// <summary>
        /// Update User Details
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="updateUser"></param>
        /// <returns>DTO.User?</returns>
        [Route("{userID}")]
        [HttpPut]
        public async Task<DTO.User?> UpdateUsers(Guid userID, [FromBody] DTO.User updateUser)
        {
            return await _userCoordinator.UpdateUser(userID, updateUser);
        }

        /// <summary>
        /// Delete User based on ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>bool</returns>
        [Route("{userID}")]
        [HttpDelete]
        public async Task<bool> DeleteUser(Guid userID)
        {
            return await _userCoordinator.DeleteUser(userID);
        }

    }
}
