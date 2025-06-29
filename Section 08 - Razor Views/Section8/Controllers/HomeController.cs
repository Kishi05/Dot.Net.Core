using Microsoft.AspNetCore.Mvc;
using Section8.Model;

namespace Section8.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            //return View();
            return RedirectToAction("StronglyTypedView");
        }

        /// <summary>
        /// View with model inside view
        /// </summary>
        /// <returns></returns>
        [Route("/model")]
        public IActionResult Model()
        {
            ViewData["Title"] = "View With Model";
            ViewBag.Type = "Practice";
            ViewData["Env"] = "Local";
            return View();
        }

        /// <summary>
        /// Strongly Typed View - model inside controller and passed to view
        /// </summary>
        /// <returns></returns>
        [Route("/stview")]
        public IActionResult StronglyTypedView()
        {
            ViewData["Title"] = "Strongly Typed View";
            List<Person> persons = new List<Person>()
            {
                new Person()
                {
                    Id = 1,
                    Name = "Sam",
                    Age = 25,
                    Gender = 0,
                    lobby = (Lobby)0
                },
                new Person()
                {
                    Id = 1,
                    Name = "William",
                    Age = 17,
                    Gender = 0,
                    lobby = (Lobby)3
                },
                new Person()
                {
                    Id = 1,
                    Name = "Jenny",
                    Age = 40,
                    Gender = (Gender)1,
                    lobby = (Lobby)1
                }
            }.OrderBy(x =>x.Age).ToList();
            return View(persons);
        }

        /// <summary>
        /// Shared View -> First it checks inside View -> Controller Name and if not present it will check in Shared
        /// </summary>
        /// <returns></returns>
        [Route("/sharedview")]
        public IActionResult SharedView()
        {
            ViewData["Title"] = "Shared View";
            ViewData["BodyText"] = "Shared View -> First it checks inside View -> Controller Name and if not present it will check in Shared";
            return View();
        }

    }
}
