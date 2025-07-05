using Microsoft.AspNetCore.Mvc;
using Section21.Filters.ActionFilters;
using Section21.Filters.AttributeActionFilter;
using Section21.Filters.AuthorizationFilter;
using Section21.Filters.OverrideDummyFilter;
using Section21.ViewModels;
using System.Xml.Serialization;

namespace Section21.Controllers
{

/* --------------------------- ServiceFilter vs TypeFilter ---------------------------  *
 * [ServiceFilter] – Resolves the filter from DI container (must be registered).        *
 *                – No support for passing custom constructor arguments.                *
 *                – Preferred when filter relies only on DI-injected services.          *
 *                                                                                      *
 * [TypeFilter]   – Creates the filter instance via reflection (DI optional).           *
 *                – Supports passing arguments via 'Arguments = new object[] { }'.      *
 *                – Useful for attribute-based customization (e.g., [Audit("Create")]). *
 *                                                                                      *
 * 🔹 Use ServiceFilter when you want DI-managed, reusable filters.                     *
 * 🔹 Use TypeFilter when you need per-use parameterization via attributes.             *
 * ----------------------------------------------------------------------------------   */


    [TypeFilter(typeof(OrderFilterAsyncActionFilter), Arguments = new object[] { "x-Filter-Level", "Class", 3 }, Order = 3)]
    [ServiceFilter(typeof(HomeAuthorizationFilter))]

    public class OrderFilterAsyncController : Controller
    {
        [TypeFilter(typeof(OrderFilterAsyncActionFilter), Arguments = new object[] { "x-Filter-Level", "Method", 1 },Order =1)]
        [SkipFilter]
        [OrderActionFilter("x-Key","x-Value")]
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
        public IActionResult Index(Book book)
        {
            string? description = ViewData["Description"]?.ToString();
            book.Description = description;
            ModelState.Clear();
            return View(book);
        }

    }
}
