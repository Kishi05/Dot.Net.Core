using Microsoft.AspNetCore.Mvc.Filters;

namespace Section21.Filters.ActionFilters
{
    public class OrderActionFilter : IActionFilter , IOrderedFilter
    {
        private readonly ILogger<OrderActionFilter> _logger;
        public readonly string _key;
        public readonly string _value;

        public int Order { get; set; }

        public OrderActionFilter(ILogger<OrderActionFilter> logger, string Key, string value, int order)
        {
            _logger = logger;
            _key = Key;
            _value = value;
            Order = order;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Key : {key} - Value : {value}", nameof(OrderActionFilter), nameof(OnActionExecuted), _key, _value);

            context.HttpContext.Response.Headers.Append(_key, _value);

            _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Action Filter Executed", nameof(OrderActionFilter), nameof(OnActionExecuted));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Action Filter Executed", nameof(OrderActionFilter), nameof(OnActionExecuted));
        }
    }
}
