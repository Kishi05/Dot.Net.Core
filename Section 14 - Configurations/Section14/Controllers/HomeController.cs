using Microsoft.AspNetCore.Mvc;
using Section14.Services.Interface;
using Section14.ViewModels;

namespace Section14.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IConfig _configInj;
        public HomeController(IConfiguration configuration, IConfig configInj)
        {
            _configuration = configuration;
            _configInj = configInj;
        }

        [Route("")]
        public IActionResult Index()
        {
            List<string> list = new List<string>();
            string value1 = _configuration["Config:Key"];
            string value2 = _configuration.GetValue<string>("Config:Key");
            string value3 = _configuration.GetValue<string>("Config:CS","Default Value");
            
            var configSection = _configuration.GetSection("Config");
            string value4 = configSection.GetValue<string>("Key");
            
            list.Add(value1);
            list.Add(value2);
            list.Add(value3);
            list.Add(value4);

            return View(list);
        }

        [Route("keyclass")]
        public IActionResult AppClass()
        {
            //AppSettings appSettings = _configuration.GetSection("Config").Get<AppSettings>();

            AppSettings appSettings = new AppSettings();

            //Bind is used to get value to existing object
            _configuration.GetSection("Config").Bind(appSettings);

            return View(appSettings);
        }

        [Route("configinj")]
        public IActionResult ConfigInj()
        {
            AppSettings appSettings = _configInj.GetValue();
            return View(appSettings);
        }

    }
}
