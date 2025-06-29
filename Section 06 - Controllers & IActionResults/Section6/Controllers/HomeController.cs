using Microsoft.AspNetCore.Mvc;
using Section6.Model;

namespace Section6.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        [Route("/")]
        public string Index()
        {
            return "Welcome";
        }

        #region IActionResult

        /// <summary>
        /// Controller with Parameters
        /// Types of Status Results
        /// IActionResult parent class for sub Types hence can use to send any type of result as return value
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="isLogged"></param>
        /// <returns></returns>
        [Route("getbook/{bookID:int?}/{isLogged:bool=false}")]
        public IActionResult GetBook(int? bookID,bool isLogged)
        {
            if (isLogged)
            {
                return Unauthorized("Invalid User");
            }
            if (!bookID.HasValue)
            {
                return BadRequest("Book ID is required");
            }
            else if (bookID.Value <= 0 || bookID > 100)
            {
                return NotFound("Book ID is invalid");
            }
            else
            {
                return File("/cat.png","image/png",$"Book_{bookID}.png");
            }
        }

        #endregion

        #region Action Result Types

        [Route("content")]
        public ContentResult ContentResultType()
        {
            //return new ContentResult()
            //{
            //    Content = "<div><h1>Hello From Index Controller Method</h1></div>",
            //    ContentType = "text/html",
            //    StatusCode = 200
            //};

            return Content("<div><h1>Hello From Index Controller Method</h1></div>", "text/html");
        }

        [Route("json")]
        public JsonResult JsonResultType()
        {
            Person person = new Person() {
                RecordID = 1,
                 FirstName = "Test",
                 LastName = "User",
                 Age = 30
            };

            //return new JsonResult(person)
            //{
            //    ContentType = "application/json",
            //    StatusCode = 200
            //};

            return Json(person);
        }

        [Route("vfile")]
        public FileResult VirtualFileResultType()
        {
            return new VirtualFileResult("/cat.png", "image/png") {
                FileDownloadName="cat.png"
            };
        }

        [Route("pfile")]
        public FileResult PhysicalFileResultType()
        {
            return new PhysicalFileResult(@"D:\cat.png", "image/png"){
                FileDownloadName="cat.png"
            };
        }

        [Route("filedownload")]
        public FileResult FileContentResultType()
        {
            var pic = System.IO.File.ReadAllBytes("/cat.png");

            return new FileContentResult(pic, "image/png")
            {
                FileDownloadName = "filecat.png"
            };
        }

        [Route("filecontent")]
        public FileResult FileResultType()
        {
            //var pic = System.IO.File.ReadAllBytes("/cat.png");

            //return new FileContentResult(pic, "image/png")
            //{
            //    FileDownloadName = "filecat.png"
            //};

            return File("/cat.png", "image/png");

        }

        #endregion

        #region Redirect

        [Route("offer")]
        public IActionResult Offer()
        {
            //return new RedirectToActionResult("expired", "home", new { },true);

            //return Redirect("expired"); //302
            //return RedirectPermanent("expired"); //301
            return RedirectToAction("expired", "home"); //302
        }

        [Route("expired")]
        public IActionResult Expired()
        {
            return Content("<h1 style= \"color:red\">Offer Expired</h1>", "text/html");
        }

        #endregion

    }
}
