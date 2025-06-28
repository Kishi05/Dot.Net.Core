/*
===============================================================================
    MODEL BINDING PRACTICE CLASS – ASP.NET Core
===============================================================================

    PURPOSE:
    This class is used for learning and practicing model binding concepts 
    in ASP.NET Core MVC/Web API.

    MODEL BINDING SOURCES (in default order of precedence):
    -------------------------------------------------------
    1. Form values     => Request.Form            [FromForm]
    2. Route data      => Route templates          [FromRoute]
    3. Query string    => Request.Query            [FromQuery]
    4. Headers         => Request.Headers          [FromHeader] (explicit only)
    5. Body            => Request.Body             [FromBody] (explicit, one only)
    6. Services        => Dependency Injection     [FromServices]

    EXPLICITLY BOUND SOURCES:
    --------------------------
    - [FromBody]       => Request.Body        (e.g., JSON in POST/PUT)
    - [FromHeader]     => Request.Headers
    - [FromServices]   => Dependency Injection

    NOTES:
    - Explicit attributes like [FromQuery], [FromBody], etc., override defaults.
    - Only one parameter per action method can be bound from the body.

    EXAMPLES:
    - [FromRoute] int id
    - [FromQuery] string search
    - [FromForm] LoginModel formData
    - [FromBody] User userModel
    - [FromHeader(Name = "X-Token")] string token
    - [FromServices] ILogger<MyController> logger

 ⚠️ NOTE:
    - Request.Body is never read automatically—must use [FromBody]
    - Only one parameter per action can be bound from [FromBody]

===============================================================================
*/

using Microsoft.AspNetCore.Mvc;
using Section7.Models;

namespace Section7.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {

        [Route("route/{id:int}")]
        public IActionResult Route([FromRoute] int id)
        {
            return Content("ID Value : " + id);
        }

        [Route("query")]
        public IActionResult Index([FromQuery]int id)
        {
            return Content("ID Value : " + id);
        }

        /// <summary>
        /// Model object will bind the value from places it can find.
        /// Here book.BookID and book.Title gets value from query and Route values resp
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="Title"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [Route("model/{Title}")]
        public IActionResult ClassModel([FromQuery] int bookid, [FromRoute] string Title, Book book)
        {
            string result = $"Query : {bookid} - Title : {Title} - Book Model [ BookID : {book.BookID} - Title : {book.Title} ]";
            
            //Output -> Query : 4 - Title : textbook - Book Model [ BookID : 4 - Title : textbook ]
            return Content(result);
        }

        /// <summary>
        /// Sample Order to identify hierarchy
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="Title"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [Route("models/{Title}")]
        public IActionResult ClassModels( int bookid, string Title, Book book)
        {
            string result = $"Query : {bookid} - Title : {Title} - Book Model [ BookID : {book.BookID} - Title : {book.Title} ]";

            //Input -> http://localhost:5023/home/model/DotNet?bookid=4&title=CSharp

            //Output -> Query : 4 - Title : DotNet - Book Model [ BookID : 4 - Title : DotNet ]

            return Content(result);
        }

        /// <summary>
        /// Sample Order to identify hierarchy
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="Title"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [Route("form")]
        public IActionResult Form(int bookid, string Title, Book book)
        {
            string result = $"Query : {bookid} - Title : {Title} - Book Model [ BookID : {book.BookID} - Title : {book.Title} ]";

            /*
                Postman
                ==========================

                Using: x-www-form-urlencoded

                Values ->
                    bookid = 5
                    Title  = DotNet

                Output ->
                    Query      : 5
                    Title      : DotNet
                    Book Model : [ BookID : 5 - Title : DotNet ]
            */

            return Content(result);
        }

        /// <summary>
        /// Data hierarchy in model
        /// Even Though we have [FromQuery] & [FromBody] inside BookWithForm -> [FromBody] from action method takes priority
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="Title"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [Route("formmodel/{title}")]
        public IActionResult FormModel(int bookid, string Title, [FromBody] BookWithFrom bookWithFrom)
        {
            string result = $"Query : {bookid} - Title : {Title} - Book Model [ BookID : {bookWithFrom.BookID} - Title : {bookWithFrom.Title} - Description : {bookWithFrom.Description}]";

            /*
                Postman
                ==========================

                URL:   http://localhost:5023/home/formmodel/CSharp?bookid=7

                Using: Query String, Route Value, Json Body

                Values ->
                    bookid = 7
                    Title  = CSharp
                    Json Body = {
                                    "bookid": 3,
                                    "title": "DotNet",
                                    "Description": "Test Description From Json Body"
                                }

                Output ->
                    Query : 7
                    Title : CSharp
                    Book Model [ BookID : 3 - Title : DotNet - Description : Test Description From Json Body]
            */

            return Content(result);
        }

    }
}
