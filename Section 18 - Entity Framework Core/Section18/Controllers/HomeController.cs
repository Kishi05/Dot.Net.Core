using Microsoft.AspNetCore.Mvc;
using Section18.Utilities;
using Section18.ViewModels;
using Services.Users.Interface;

namespace Section18.Controllers
{
    public class HomeController : Controller
    {
        public readonly IUsers _users;
        public HomeController(IUsers users) => _users = users;

        public IActionResult Index()
        {
            return RedirectToAction("Search");
        }

        public IActionResult Registration(int? userID)
        {
            ViewBag.isPost = false;
            UserViewModel? userViewModel = new UserViewModel();
            if (userID != null)
            {
                userViewModel = _users.GetUserByID(userID)?.ToViewModel();
            }
            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult Registration(UserViewModel registrationViewModel)
        {
            TempData["Success"] = false;
            ViewBag.isPost = true;
            if (registrationViewModel == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                UserViewModel? model = _users.AddEditUser(registrationViewModel.ToEntity())?.ToViewModel();
                if (model != null)
                {
                    TempData["Success"] = true;
                    return RedirectToAction("Registration", "Home", new { userID = model.Id });
                }
            }
            return View(registrationViewModel);
        }

        public IActionResult Search()
        {
            List<UserViewModel> list = _users.GetUsers().Select(x => x.ToViewModel()).ToList();
            return View(list);
        }

        public IActionResult Delete(int? userID)
        {
            bool result = _users.DeleteUser(userID);
            if (result)
            {
                return RedirectToAction("Search");
            }
            return StatusCode(500);
        }
    }
}
