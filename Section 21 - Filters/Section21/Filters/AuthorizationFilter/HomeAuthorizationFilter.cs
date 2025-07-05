using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Section21.Filters.OverrideDummyFilter;

namespace Section21.Filters.AuthorizationFilter
{
    public class HomeAuthorizationFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if(!context.Filters.OfType<SkipFilter>().Any())
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
                        /* ----------------------- Short Circuting ----------------------- */
                        // Short-circuiting: stop the pipeline early by setting context.Result before calling next().
                        // Short-circuiting skips the rest of the pipeline by setting context.Result directly.

                        context.Result = new UnauthorizedResult();
                        return;
                    }
                }
            }
        }
    }
}
