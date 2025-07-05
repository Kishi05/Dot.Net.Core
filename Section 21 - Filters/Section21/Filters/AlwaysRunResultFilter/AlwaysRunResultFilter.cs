using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Section21.Filters.ResourceFilters;

namespace Section21.Filters.AlwaysRunResultFilter
{
    public class AlwaysRunResultFilter : IAsyncAlwaysRunResultFilter
    {
        private readonly ILogger<AlwaysRunResultFilter> _logger;

        public AlwaysRunResultFilter(ILogger<AlwaysRunResultFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            _logger.LogInformation("Before - {FilterName}.{MethodName}", nameof(HomeResourceFilter), nameof(OnResultExecutionAsync));
            context.Result = new ContentResult {
                Content = "Always Run Result View Reached",
                StatusCode = 401,
                ContentType = "text/plain"
            };
            await next();
        }
    }
}
