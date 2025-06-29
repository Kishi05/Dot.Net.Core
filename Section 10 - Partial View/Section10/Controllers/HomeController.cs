using Microsoft.AspNetCore.Mvc;
using Section10.ViewModels;

namespace Section10.Controllers
{
    /// <summary>
    /// No Controller Route - Can access all below Actions as "localhost:{portNo}/{ActionRoute}"
    /// Eg : http://localhost:5186/StronglyTyped
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Calling Partial View from View using ViewData
        /// </summary>
        /// <returns></returns>
        [Route("/")]
        public IActionResult Index()
        {
            var cities = new List<City>
            {
                new City { Id = 1, Name = "New York" },
                new City { Id = 2, Name = "London" },
                new City { Id = 3, Name = "Tokyo" }
            };
            ViewData["Cities"] = cities;
            return View();
        }

        /// <summary>
        /// Calling Partial View from View using Model
        /// </summary>
        /// <returns></returns>
        [Route("/StronglyTyped")]
        public IActionResult StronglyTyped()
        {
            var cities = new List<City>
            {
                new City { Id = 1, Name = "New York" },
                new City { Id = 2, Name = "London" },
                new City { Id = 3, Name = "Tokyo" }
            };
            return View(cities);
        }

        /// <summary>
        /// Action to send Partial View - HTML as response
        /// Can also be called seperately
        /// Can also view on browser seperately
        /// </summary>
        /// <returns></returns>
        [Route("/PartialViewReturn")]
        public IActionResult PartialViewReturn()
        {
            var cities = new List<City>
            {
                new City { Id = 1, Name = "New York" },
                new City { Id = 2, Name = "London" },
                new City { Id = 3, Name = "Tokyo" }
            };
            return PartialView("_CityGridPartialViewST",cities);
        }

        /// <summary>
        /// Call above [PartialViewReturn] Action inside view and load async to current View
        /// </summary>
        /// <returns></returns>
        [Route("/PartialViewConsume")]
        public IActionResult PartialViewConsume()
        {
            return View();
        }

    }
}
