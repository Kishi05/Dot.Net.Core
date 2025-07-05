using Microsoft.AspNetCore.Mvc.Filters;

namespace Section21.Filters.FilterFactory
{
    public class FilterFactory : Attribute, IFilterFactory
    {
        private string _key { get; set; }
        private string _value { get; set; }
        public FilterFactory(string key, string value) 
        {
            _key = key;
            _value = value;
        }
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            HomeArgsFilter? instance = serviceProvider.GetService<HomeArgsFilter>();
            instance.Key = _key;
            instance.Value = _value;
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
        private string _value { get; set; }

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public string Value {
            get { return _value; }
            set { _value = value; }
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("Before : IFactoryFilter - {FileName}.{MethodName}",nameof(HomeArgsFilter), nameof(OnActionExecutionAsync));

            await next();

            _logger.LogInformation("After : IFactoryFilter - {FileName}.{MethodName}", nameof(HomeArgsFilter), nameof(OnActionExecutionAsync));
        }
    }
}
