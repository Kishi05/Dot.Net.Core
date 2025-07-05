using Microsoft.AspNetCore.Mvc.Filters;

namespace Section21.Filters.ActionFilters
{
    public class FilterArgumentActionFilter : IActionFilter
    {
        private readonly ILogger<FilterArgumentActionFilter> _logger;
        public readonly string _key;
        public readonly string _value;

        public FilterArgumentActionFilter(ILogger<FilterArgumentActionFilter> logger, string Key, string value)
        {
            _logger = logger;
            _key = Key;
            _value = value;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Key : {key} - Value : {value}", nameof(FilterArgumentActionFilter), nameof(OnActionExecuted), _key, _value);

            context.HttpContext.Response.Headers.Append(_key, _value);

            _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Action Filter Executed", nameof(FilterArgumentActionFilter), nameof(OnActionExecuted));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Action Filter Executed", nameof(FilterArgumentActionFilter), nameof(OnActionExecuted));
        }
    }
}
