using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Section29.Core.DTO;
using Section29.Core.Entities;
using Section29.Core.Identity;
using Section29.Core.ServiceContracts.Interface;

namespace Section29.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class HomeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IJwtService _jwtService;

        public HomeController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, ILogger<HomeController> logger, IJwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            _jwtService = jwtService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUser>> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                string error = string.Join("|", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return ValidationProblem(error);
            }

            ApplicationUser user = new ApplicationUser()
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                PersonName = registerDTO.PersonName,
                PhoneNumber = registerDTO.Phone
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                var authResponse = _jwtService.CreateJWTToken(user);
                return Ok(authResponse);
            }
            else
            {
                string identityError = string.Join("|", result.Errors.Select(e => e.Description));
                return Problem(identityError);
            }
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUser>> Login(LoginDTO loginDTO)
        {
            if (loginDTO is null) return BadRequest();

            if (!ModelState.IsValid)
                return Problem(string.Join("|", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));

            ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user is null) return BadRequest("Invalid UserName or Password");

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email,loginDTO.Password, isPersistent: false,lockoutOnFailure:true);

            if (result.Succeeded)
            {
                var authResponse = _jwtService.CreateJWTToken(user);
                return Ok(authResponse);
            }

            return Unauthorized();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<bool> IsEmailAvailable(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user is null) return true;
            return false;
        }

        [HttpPost]
        [Route("Logout")]
        [AllowAnonymous]
        public async Task<bool> LogOut()
        {
            await _signInManager.SignOutAsync();

            return true;
        }

        [HttpGet]
        [Route("UserList")]
        [Authorize]
        public List<UserDTO> UserList()
        {
            List<UserDTO> list = new List<UserDTO>()
            {
                new UserDTO()
                {
                    RecordID = Guid.NewGuid(),
                    Name = "Test",
                    Email = "Test@dnet.com",
                    Country = "UK",
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow
                }
            };
            return list;
        }

    }
}
