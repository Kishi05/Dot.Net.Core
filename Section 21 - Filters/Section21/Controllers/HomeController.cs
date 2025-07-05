using Microsoft.AspNetCore.Mvc;
using Section21.Filters.ActionFilters;
using Section21.Filters.AuthorizationFilter;
using Section21.Filters.ExceptionFilters;
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
    [TypeFilter(typeof(HomeAuthorizationFilter))]
    [TypeFilter(typeof(HomeExceptionFilter))]
    public class HomeController : Controller
    {
                                /*
                                 * -------------- Method Level Filter --------------
                                 * Only applies to applied method.
                                 */
        [TypeFilter(typeof(HomeActionFilter))] //Handle Query String Example
        [TypeFilter(typeof(FilterArgumentActionFilter), Arguments = new object[] { "x-Filter-Level", "Method"})] //Filter Arguments
        [TypeFilter(typeof(HomeResultFilter))]
        [TypeFilter(typeof(HomeResourceFilter))]
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
        public IActionResult Index(Book book)
        {
            string? description = ViewData["Description"]?.ToString();
            book.Description = description;
            ModelState.Clear();
            return View(book);
        }

    }
}
