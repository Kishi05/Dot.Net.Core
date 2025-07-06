using Microsoft.AspNetCore.Mvc;
//using Section25.Utilities;
using Section25.ViewModels;
using BusinessLayer.Interface;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Section25.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(UserViewModel registrationViewModel)
        {
            TempData["Success"] = false;
            ViewBag.isPost = true;
            if (registrationViewModel == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {

                ApplicationUser user = new ApplicationUser()
                {
                    Email = registrationViewModel.Email,
                    PhoneNumber = registrationViewModel.Phone,
                    UserName = registrationViewModel.Email,
                    Name = registrationViewModel.Name
                };

                IdentityResult result = await _userManager.CreateAsync(user, registrationViewModel.Password);

                if (result.Succeeded)
                {
                    // Issue the auth cookie **after** the Identity user record is created
                    // (i.e., user.Id, SecurityStamp, etc. already exist).
                    await _signInManager.SignInAsync(user, isPersistent: false);   // call only once, right after creation


                    //Role Creation

                    ApplicationRole? role = await _roleManager.FindByNameAsync(registrationViewModel.Role.ToString());
                    if (role is null)
                    {
                        ApplicationRole adminRole = new ApplicationRole()
                        {
                            Name = registrationViewModel.Role.ToString()
                        };
                        IdentityResult roleCreateResult = await _roleManager.CreateAsync(adminRole);
                        if (!roleCreateResult.Succeeded)
                        {
                            foreach (IdentityError error in result.Errors)
                            {
                                ModelState.AddModelError("Register", error.Description);
                            }
                            return View(registrationViewModel);
                        }
                    }

                    IdentityResult roleResult = await _userManager.AddToRoleAsync(user, registrationViewModel.Role.ToString());
                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Search", "Home");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("Register", error.Description);
                        }
                        return View(registrationViewModel);
                    }
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("Register", error.Description);
                    }
                    return View(registrationViewModel);
                }
            }
            return View(registrationViewModel);
        }

        [Authorize("NotAuthenticated")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string? ReturnUrl)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = loginViewModel.Email;
            user.Email = loginViewModel.Email;
            try
            {
                // Validate password and sign‑in in one shot
                var result = await _signInManager.PasswordSignInAsync(
                                 loginViewModel.Email,           // userName / email
                                 loginViewModel.Password,        // password
                                 isPersistent: false,
                                 lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    if (ReturnUrl is not null && string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                    else
                    {
                        var isAdmin = await _userManager.IsInRoleAsync(user,AdminEnum.Admin.ToString());
                        if (isAdmin)
                            return RedirectToAction("Index", "Admin",new {area = "Admin" });
                    }
                        return RedirectToAction("Search", "Home");
                }
            }
            catch (Exception ex)
            {

            }
            return Unauthorized();
        }

        public IActionResult Search()
        {
            //Dummy Method for this section
            List<UserViewModel>? list = null;
            return View(list);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> IsEMailRegistered(string Email)
        {
            if (Email is not null)
            {
                ApplicationUser? user = await _userManager.FindByEmailAsync(Email);
                if (user is not null)
                {
                    return Json(false);
                }
            }
            return Json(true);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
