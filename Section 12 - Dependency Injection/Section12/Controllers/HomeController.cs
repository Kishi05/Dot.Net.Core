using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interface;

namespace Section12.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProgramLanguageService _languageService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public HomeController(IProgramLanguageService languageService, IServiceScopeFactory serviceScopeFactory)
        {
            _languageService = languageService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [Route("/")]
        public IActionResult Index()
        {
            List<string> languages = _languageService.GetLanguages();
            return View(languages);
        }

        [Route("/check")]
        public IActionResult Check()
        {
            return View();
        }

        /// <summary>
        /// Creating Dependecy Injection with in using to manage Dispose effectively
        /// </summary>
        /// <returns></returns>
        [Route("/usingcheck")]
        public IActionResult UsingCheck()
        {
            List<Guid> ids = new List<Guid>();
            ids.Add(_languageService.GetID());
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                IProgramLanguageService serviceObj = scope.ServiceProvider.GetRequiredService<IProgramLanguageService>();
                var guid = serviceObj.GetID();
                ids.Add(guid);
            }
            
                return View(ids);
        }

        /// <summary>
        /// Creating Dependecy Injection in View
        /// </summary>
        /// <returns></returns>
        [Route("/ViewDI")]
        public IActionResult ViewDI()
        {
            return View();
        }

    }
}
