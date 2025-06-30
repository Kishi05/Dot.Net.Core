using Microsoft.AspNetCore.Mvc;

namespace Section13.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("/")]
        [Route("Dummy")]
        public IActionResult Index()
        {
            //Usage of Environment in Controller with Dependency Injection
            Dictionary<string, string> EnvValues = new Dictionary<string, string>() {
                {nameof(_webHostEnvironment.ContentRootPath),_webHostEnvironment.ContentRootPath },
                {nameof(_webHostEnvironment.ApplicationName),_webHostEnvironment.ApplicationName },
                {nameof(_webHostEnvironment.EnvironmentName),_webHostEnvironment.EnvironmentName },
                {nameof(_webHostEnvironment.WebRootPath),_webHostEnvironment.WebRootPath }
            };
            ViewData["Env"] = EnvValues;
            return View();
        }

        [Route("Dummy")]
        public IActionResult Dummy()
        {
            return View();
        }

    }
}
