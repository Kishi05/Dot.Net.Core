using Microsoft.AspNetCore.Mvc.Filters;

namespace Section21.Filters.AttributeActionFilter
{
    public class OrderActionFilter : ActionFilterAttribute
    {
        private string _key;
        private string _value;
        public OrderActionFilter(string key, string value) 
        {
            _key = key;
            _value = value;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Response.Headers.Append(_key, _value);
            await next();
        }


    }
}
