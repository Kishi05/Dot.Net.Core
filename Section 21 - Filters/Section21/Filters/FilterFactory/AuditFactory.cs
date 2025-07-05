using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Section21.Filters.FilterFactory
{
    public class AuditFactory : Attribute, IFilterFactory
    {
        private string _key { get; set; }
        public AuditFactory(string key) 
        {
            _key = key;
        }
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            HomeArgsFilter? instance = serviceProvider.GetService<HomeArgsFilter>();
            instance.Key = _key;
            return instance;
        }
    }
    public class HomeArgsFilter : IAsyncActionFilter
    {
        private readonly ILogger<HomeArgsFilter> _logger;
        public HomeArgsFilter(ILogger<HomeArgsFilter> logger)
        {
            _logger = logger;
        }
        private string _key {  get; set; }

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_key.ToUpper() == "BLOCK")
            {
                context.Result = new ContentResult { Content = "Blocked", StatusCode = 403 };
                return;
            }

            _logger.LogInformation("Before : IFactoryFilter - {FileName}.{MethodName}", nameof(HomeArgsFilter), nameof(OnActionExecutionAsync));

            await next();

            _logger.LogInformation("After : IFactoryFilter - {FileName}.{MethodName}", nameof(HomeArgsFilter), nameof(OnActionExecutionAsync));
        }
    }
}
