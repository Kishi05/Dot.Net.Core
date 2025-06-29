using Microsoft.AspNetCore.Mvc;
using Section11.ViewModels;

namespace Section11.Controllers
{
    public class HomeController : Controller
    {
        private List<ProgramLang> programs = new List<ProgramLang>() {
                new ProgramLang()
                {
                    ID = 1,
                    Name = "C"
                },
                new ProgramLang()
                {
                    ID = 2,
                    Name = "C++"
                },
                new ProgramLang()
                {
                    ID = 3,
                    Name = "C#"
                },
                new ProgramLang()
                {
                    ID = 4,
                    Name = "JAVA"
                },
                new ProgramLang()
                {
                    ID = 5,
                    Name = "Python"
                },
                new ProgramLang()
                {
                    ID = 6,
                    Name = "JavaScript"
                }
            };

        /// <summary>
        /// Load View Component using ViewData
        /// </summary>
        /// <returns></returns>
        [Route("/")]
        public IActionResult Index()
        {
            
            ViewData["programs"] = programs;
            return View();
        }

        /// <summary>
        /// Load View Component using View Model
        /// </summary>
        /// <returns></returns>
        [Route("/StronglyTyped")]
        public IActionResult StronglyTyped()
        {
            return View(programs);
        }

        /// <summary>
        /// Return View Component as ActionResult
        /// </summary>
        /// <returns></returns>
        [Route("/ViewComponent")]
        public IActionResult ViewComponent()
        {
            programs.Add(new ProgramLang()
            {
                ID = 7,
                Name="GO"
            });
            return ViewComponent("StronglyTypedGrid",programs);
        }

    }
}
