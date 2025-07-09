namespace Section30.Endpoint_filter
{
    public class CustomEndPoint : IEndpointFilter
    {
        private readonly ILogger<CustomEndPoint> _logger;
        public CustomEndPoint(ILogger<CustomEndPoint> logger)
        {
            _logger = logger;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            //Hits First
            _logger.LogInformation("Endpoint Filter - Before Logic");

            var result = await next(context);

            //Hits Fifth
            _logger.LogInformation("Endpoint Filter - After Logic");

            return result;
        }
    }
}
