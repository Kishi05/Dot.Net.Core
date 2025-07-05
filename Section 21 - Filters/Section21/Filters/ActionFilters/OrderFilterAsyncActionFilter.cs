using Microsoft.AspNetCore.Mvc.Filters;

namespace Section21.Filters.ActionFilters
{
    public class OrderFilterAsyncActionFilter : IAsyncActionFilter , IOrderedFilter
    {
        private readonly ILogger<OrderFilterAsyncActionFilter> _logger;
        public readonly string _key;
        public readonly string _value;

        public int Order { get; }

        public OrderFilterAsyncActionFilter(ILogger<OrderFilterAsyncActionFilter> logger, string Key, string value, int order)
        {
            _logger = logger;
            _key = Key;
            _value = value;
            Order = order;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Before Hitting Action Method - OnActionExecuting
            _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Action Filter Executed", nameof(OrderFilterAsyncActionFilter), nameof(OnActionExecutionAsync));

            await next(); // Responsible to continue the operation, if not present execution stops here only.

            // After Hitting Action Method  - OnActionExecuted

            _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Key : {key} - Value : {value}", nameof(OrderFilterAsyncActionFilter), nameof(OnActionExecutionAsync), _key, _value);

            context.HttpContext.Response.Headers.Append(_key, _value);

            _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Action Filter Executed", nameof(OrderFilterAsyncActionFilter), nameof(OnActionExecutionAsync));
        }
    }
}
