
/*---------------------------------------------------------------------------
 * EXCEPTION‑FILTER BEHAVIOR – QUICK REFERENCE
 *
 * • Exception filters fire ONLY for exceptions thrown during:
 *      – Action methods
 *      – Action filters (On[Action]Executing / On[Action]Executed)
 *
 * • They do NOT see exceptions thrown in:
 *      – Authorization filters
 *      – Resource filters
 *      – Result filters
 *      – View / formatter execution (ViewNotFound, JSON serialization errors, etc.)
 *
 * • If you need to handle those later‑pipeline errors, use:
 *      – A Result filter   → inspect ResultExecutedContext.Exception
 *      – Global middleware → app.UseExceptionHandler(), app.UseDeveloperExceptionPage()
 *
 * HOW TO TEST:
 * 1.  Throw an exception in an action filter →  Exception filter catches it ✔
 * 2.  Throw in a resource filter            →  Skips exception filter ❌
 * 3.  Return View("MissingView")            →  Skips exception filter ❌
 *
 *---------------------------------------------------------------------------*/


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Section21.Filters.ResourceFilters;
using System.Net;

namespace Section21.Filters.ExceptionFilters
{
    public class HomeExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<HomeExceptionFilter> _logger;

        public HomeExceptionFilter(ILogger<HomeExceptionFilter> logger)
        {
            _logger = logger;
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogInformation("Before - {FilterName}.{MethodName}", nameof(HomeResourceFilter), nameof(OnExceptionAsync));
             
            _logger.LogError("Exception Handler - {FilterName}.{MethodName}. {ErrorType} - {ErrorMessage}", nameof(HomeResourceFilter), nameof(OnExceptionAsync), context.Exception.GetType().ToString(), context.Exception.Message);

            context.Result = new StatusCodeResult((int)HttpStatusCode.InternalServerError);

            return Task.CompletedTask;

        }
    }
}
