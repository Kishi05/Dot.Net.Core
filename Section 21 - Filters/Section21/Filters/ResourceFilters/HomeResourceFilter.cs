using Microsoft.AspNetCore.Mvc.Filters;
using Section21.Filters.ActionFilters;

namespace Section21.Filters.ResourceFilters
{
    public class HomeResourceFilter : IAsyncResourceFilter
    {
        private readonly ILogger<HomeResourceFilter> _logger;

        public HomeResourceFilter(ILogger<HomeResourceFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            _logger.LogInformation("Before - {FilterName}.{MethodName}", nameof(HomeResourceFilter), nameof(OnResourceExecutionAsync));

            await next();

            _logger.LogInformation("Before - {FilterName}.{MethodName}", nameof(HomeResourceFilter), nameof(OnResourceExecutionAsync));
        }
    }
}
