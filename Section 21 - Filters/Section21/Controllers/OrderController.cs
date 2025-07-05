using Microsoft.AspNetCore.Mvc;
using Section21.Filters.ActionFilters;
using Section21.ViewModels;

namespace Section21.Controllers
{
    /*                                        ------ Custom Order Of Filters ------
     * ----------------------------------------------------------------------------------------------------------------------
     *                                                                                                                      |
     *                                                  Global   =   0                                                      |
     *                                                  Class    =   3                                                      |
     *                                                  Method   =  -1                                                      |
     * ----------------------------------------------------------------------------------------------------------------------
     *                                                  Execution Order                                                     |
     * ----------------------------------------------------------------------------------------------------------------------
     *                                                                                                                      |
     *      Method(-1)  ->  Global(0)  ->  Class(3)  ->  | Action Method |  ->  Class(3)  ->  Global(0)  ->  Method(-1)     |
     *                                                                                                                      |
     * ----------------------------------------------------------------------------------------------------------------------
     */


    [TypeFilter(typeof(FilterArgumentActionFilter), Arguments = new object[] { "x-Filter-Level", "Class" },Order = 3)] // Class Level Filter
    public class OrderController : Controller
    {
        [TypeFilter(typeof(FilterArgumentActionFilter), Arguments = new object[] { "x-Filter-Level", "Method"}, Order = -1)] // Method Level
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
