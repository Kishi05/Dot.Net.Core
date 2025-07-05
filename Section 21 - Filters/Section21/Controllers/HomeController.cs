using Microsoft.AspNetCore.Mvc;
using Section21.Filters.ActionFilters;
using Section21.Filters.AlwaysRunResultFilter;
using Section21.Filters.AuthorizationFilter;
using Section21.Filters.ExceptionFilters;
using Section21.Filters.OverrideDummyFilter;
using Section21.Filters.ResourceFilters;
using Section21.Filters.ResultFilters;
using Section21.ViewModels;

namespace Section21.Controllers
{
   /*
    * -------------- Class Level Filter --------------
    * Will apply for all methods inside this class
    */

    [TypeFilter(typeof(FilterArgumentActionFilter), Arguments = new object[] { "x-Filter-Level", "Class" })] // <- Class Level Filter
    [TypeFilter(typeof(HomeExceptionFilter))]
    [TypeFilter(typeof(HomeAuthorizationFilter))]
    public class HomeController : Controller
    {

       /*
        * -------------- Method Level Filter --------------
        * Only applies to applied method.
        */

        [TypeFilter(typeof(HomeActionFilter))] //Handle Query String Example
        [TypeFilter(typeof(FilterArgumentActionFilter), Arguments = new object[] { "x-Filter-Level", "Method"})] //Filter Arguments
        [TypeFilter(typeof(HomeResourceFilter))]

        #region Testing Always Run Result Filter

        // Comment below lines and delete Cookie if present
        // On Post -> Auth Filter should fail due to cookie not found and move to AlwaysRunResultFilter Even after Short Circuting
        // Normal Filters and Exception Handler will not catch this error

        //[TypeFilter(typeof(HomeResultFilter))]
        //[SkipFilter] <- This is checked inside Authorization Filter. Un Check to skip Auth check

        #endregion

        public IActionResult Index(int? id)
        {
            Book book = new Book() {
                BookID = 1,
                Title = "Harry Potter",
                Author="J.K.Rowling",
                Description="Adventures in Magical World",
                price = 1000
            };
            return View(book);
        }

        [HttpPost]
        [TypeFilter(typeof(HomeActionFilter))]  //Handle Object Example
        [TypeFilter(typeof(HomeResultFilter))]
        [TypeFilter(typeof(HomeResourceFilter))]
        [TypeFilter(typeof(HomeAuthorizationFilter))]
        [TypeFilter(typeof(AlwaysRunResultFilter))]

        public IActionResult Index(Book book)
        {
            string? description = ViewData["Description"]?.ToString();
            book.Description = description;
            ModelState.Clear();
            return View(book);
        }

    }
}
