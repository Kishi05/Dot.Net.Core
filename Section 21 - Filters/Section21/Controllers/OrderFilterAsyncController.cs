using Microsoft.AspNetCore.Mvc;
using Section21.Filters.ActionFilters;
using Section21.ViewModels;

namespace Section21.Controllers
{
    [TypeFilter(typeof(OrderFilterAsyncActionFilter), Arguments = new object[] { "x-Filter-Level", "Class", 3 }, Order = 3)]
    public class OrderFilterAsyncController : Controller
    {
        [TypeFilter(typeof(OrderFilterAsyncActionFilter), Arguments = new object[] { "x-Filter-Level", "Method", 1 },Order =1)]
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
