using Microsoft.AspNetCore.Mvc;
using Section7.CustomModelBuilder;
using Section7.Models;

namespace Section7.Controllers
{
    [Route("CustomModelBuilder")]
    public class CustomModelBuilderController : Controller
    {
        [Route("Index")]
        public IActionResult Index([ModelBinder(binderType:typeof(RegisterCustomBinder))] RegisterCommon register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList());
            }
            return Json(register);
        }

        [Route("CMBProvider")]
        public IActionResult CMBProvider(RegisterCommon register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList());
            }
            return Json(register);
        }

    }
}
