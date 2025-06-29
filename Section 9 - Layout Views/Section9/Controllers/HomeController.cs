using Microsoft.AspNetCore.Mvc;
using Section9.ViewModel;

namespace Section9.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("Index")]
        public IActionResult Index()
        {
            ViewData["isAuthenticated"] = false;
            return View();
        }

        [HttpPost]
        [Route("Index")]
        public IActionResult Index(LoginViewModel loginViewModel)
        {
            ViewData["isAuthenticated"] = false;
            bool isAuth = false;

            if (ModelState.IsValid)
            {
                if (loginViewModel != null)
                {
                    if (loginViewModel.Email == "admin@net.com" && loginViewModel.Password == "Admin@123")
                    {
                        isAuth = true;
                    }
                }
            }

            return (isAuth) ? RedirectToAction("Launch", "Home") : View(loginViewModel);
        }

        [Route("Launch")]
        public IActionResult Launch()
        {
            ViewData["isAuthenticated"] = true;
            return View();
        }

        [Route("Search")]
        public IActionResult Search()
        {
            ViewData["isAuthenticated"] = true;
            List<UserDirectoryViewModel> users = new List<UserDirectoryViewModel>() {
                new UserDirectoryViewModel
                {
                    Id = 1,
                    Email="sam@net.com",
                    Name="Sam",
                    Role="Lead"
                },
                new UserDirectoryViewModel
                {
                    Id = 2,
                    Email="luke@net.com",
                    Name="Luke",
                    Role="Manager"
                },
                new UserDirectoryViewModel
                {
                    Id = 3,
                    Email="Jake@net.com",
                    Name="Jake",
                    Role="Engineer"
                }
            };
            return View(users);
        }
    }
}
