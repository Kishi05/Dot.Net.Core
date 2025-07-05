using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Section21.Filters.AuthorizationFilter
{
    public class HomeAuthorizationFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Cookies.ContainsKey("Auth-Token"))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            else
            {
                if (context.HttpContext.Request.Cookies["Auth-Token"] != "DU05")
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
        }
    }
}
