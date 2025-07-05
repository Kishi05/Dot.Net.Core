using Microsoft.AspNetCore.Mvc.Filters;
using Section21.Filters.ResourceFilters;

namespace Section21.Filters.ResultFilters
{
    public class HomeResultFilter : IAsyncResultFilter
    {
        private readonly ILogger<HomeResultFilter> _logger;
        public HomeResultFilter(ILogger<HomeResultFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            _logger.LogInformation("Before - {FilterName}.{MethodName}", nameof(HomeResourceFilter), nameof(OnResultExecutionAsync));

            context.HttpContext.Response.Headers["Result-Filter"] = "Reached";

            context.HttpContext.Response.Cookies.Append("Auth-Token", "DU05");

            await next();

            _logger.LogInformation("After - {FilterName}.{MethodName}", nameof(HomeResourceFilter), nameof(OnResultExecutionAsync));
        }
    }
}
