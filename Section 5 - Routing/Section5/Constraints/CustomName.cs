
namespace Section5.Constraints
{
    public class CustomName : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            bool result = false;

            if (routeKey != null && values.ContainsKey(routeKey)) 
            {
                result = (values[routeKey] != null && values[routeKey].ToString().Equals("TestUser")) ? true : false;
            }

            return result;
        }
    }
}
