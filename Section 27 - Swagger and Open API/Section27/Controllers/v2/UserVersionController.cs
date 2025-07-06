using Microsoft.AspNetCore.Mvc;
using Section27.BL.Interface;

namespace Section27.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
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


    }
}
