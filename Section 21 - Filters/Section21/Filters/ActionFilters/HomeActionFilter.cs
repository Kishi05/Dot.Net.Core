using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Section21.ViewModels;

namespace Section21.Filters.ActionFilters
{
    public class HomeActionFilter : IActionFilter
    {
        private readonly ILogger<HomeActionFilter> _logger;
        public HomeActionFilter(ILogger<HomeActionFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

            /* ------------------------------------------------------------------------------------------------------------------------------------*/

                                        /*                          Serilog Structured logging
                                         * -------------------------------------------------------------------------------
                                         * Adds these properties {FilterName} , {MethodName} as event properties in serilog.
                                         * So we can search using FilterName = "HomeActionFilter"
                                         * Check README for screenshot of how this is used.
                                         * --------------------------------------------------------------------------------
                                         */

            _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Action Filter Executed", nameof(HomeActionFilter), nameof(OnActionExecuted));

            /* ------------------------------------------------------------------------------------------------------------------------------------*/


            // UnBox and consume
            if (context.HttpContext.Items.ContainsKey("PageTitle"))
            {
                ((Controller)context.Controller).ViewData["Title"] = context.HttpContext.Items["PageTitle"];
            }

            // UnBox it to resp Object type and consume
            if (context.HttpContext.Items.ContainsKey("BookObj"))
            {
                Book? bookObj = (Book?)context.HttpContext.Items["BookObj"];
                if (bookObj is not null)
                {
                    ((Controller)context.Controller).ViewData["BookObj"] = bookObj.Title;
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            /*
             * This Method will be invoked before Home -> Index Action Method bring hit.
             * This handles two type of input data => Query String and Post Object
             */

            _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Action Filter Executing", nameof(HomeActionFilter), nameof(OnActionExecuting));

            //Check for Model State
            if (context.ModelState.IsValid)
            {
                //Handle Post Object
                object? objValue = context.ActionArguments.Values.FirstOrDefault();
                if (objValue is Book obj)
                {
                    if (obj is not null)
                    {
                        _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Book : {objectVal}", nameof(HomeActionFilter), nameof(OnActionExecuting), obj);

                        // Differnt ways to send Data
                        ((Controller)context.Controller).ViewData["Description"] = $"{obj.Title} - {obj.Author}";   // For MVC views only (.cshtml, layout, partials).
                        context.HttpContext.Items["PageTitle"] = $"{obj.Title} - {obj.Author}";     // For anything in the pipeline—middleware, filters, controller, view, endpoint. - Single Value
                        context.HttpContext.Items["BookObj"] = obj; // For anything in the pipeline—middleware, filters, controller, view, endpoint. - Object
                    }
                    else
                    {
                        _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Book Obj is null", nameof(HomeActionFilter), nameof(OnActionExecuting));
                    }
                }
                else
                {
                    //Handle Query String
                    if(context.ActionArguments.ContainsKey("id")) 
                        _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Search ID : {id}", nameof(HomeActionFilter), nameof(OnActionExecuting), context.ActionArguments["id"]);
                    else
                        _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Paramter is not found", nameof(HomeActionFilter), nameof(OnActionExecuting));
                }
            }
            else
            {
                // Short Circuiting Action Filters
                // Can also send => new BadRequestObjectResult("Model Data is InValid");
                context.Result = ((Controller)context.Controller).View(context.ActionArguments["book"]);

                _logger.LogDebug("Serilog - {FilterName}.{MethodName} - Model State is invalid", nameof(HomeActionFilter), nameof(OnActionExecuting));
            }          
        }
    }
}
