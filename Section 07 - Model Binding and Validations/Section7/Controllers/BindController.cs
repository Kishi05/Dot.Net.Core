using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Section7.Models;
using Section7.Models.Validation;

namespace Section7.Controllers
{
    /*
                                Json
            =============================================
                    {
                        "id": 47,
                        "Name": "Test User",
                        "Phone": "8562457854",
                        "Email": "sam@sample.com"
                    }
    
                                XML
            =============================================
                    <BindTest>
                        <Id>47</Id>
                        <Name>Test User</Name>
                        <Phone>8562457854</Phone>
                        <Email>sam@sample.com</Email>
                    </BindTest>
     */

    [Route("bind")]
    //Bind ignores annotations as well. Eg - Required 
    public class BindController : Controller
    {
        [Route("Index")]
        public IActionResult Index([Bind("Name,Country")] Register register)
        {
            return Json(register);
        }

        [Route("BindNever")]
        //Bind all except thats marked as Bind Never
        public IActionResult BindNever([FromBody] BindTest bindTest)
        {
            return Content(bindTest.ToString());
        }

        [Route("Collection")]
        public IActionResult CollectionBind(CollectionBinderModel binderModel)
        {
            return Json(binderModel);
        }

        [Route("Header")]
        public IActionResult HeaderBind([FromHeader(Name ="User-Agent")] string user_Agent)
        {
            // Output using PostMan - > PostmanRuntime/7.44.1
            return Content(user_Agent);
        }

    }

    public class BindTest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [BindNever]
        public string Email { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"Id : {Id}\nName : {Name}\nEmail : {Email}\nPhone : {Phone}";
        }
    }
}
