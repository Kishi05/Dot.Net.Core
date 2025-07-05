using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace Section22.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            IExceptionHandlerFeature exceptionFeature = HttpContext.Features.GetRequiredFeature<IExceptionHandlerPathFeature>();
            return Content(exceptionFeature.Error.Message);
        }
    }
}
