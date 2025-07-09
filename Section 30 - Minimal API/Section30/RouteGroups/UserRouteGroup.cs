namespace Section30.RouteGroups
{
    public static class UserRouteGroup
    {
        public static RouteGroupBuilder UserAPI(this RouteGroupBuilder builder)
        {
            builder.MapGet("/{id:int}", async (HttpContext context, int id) =>
            {
                await context.Response.WriteAsync("Inside static RouteGroupBuilder class");
            });

            return builder;
        }
    }
}
