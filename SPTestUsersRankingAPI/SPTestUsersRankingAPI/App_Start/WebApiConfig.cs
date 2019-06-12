using SPTestUsersRankingAPI.App_Start;
using System.Web.Http;

namespace SPTestUsersRankingAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new AuthenticationFilter());
        }
    }
}
