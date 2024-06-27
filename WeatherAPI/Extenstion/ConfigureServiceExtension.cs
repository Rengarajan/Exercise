using GraphQL;
using WeatherAPI.Business.Services;
using WeatherAPI.GraphQLModels.Types;
using WeatherAPI.Shared.Interfaces;

namespace WeatherAPI.Extenstion
{
    /// <summary>
    /// Extension methods to configure custom services and GraphQL services in an IServiceCollection.
    /// </summary>
    public static class ConfigureServiceExtension
    {
        /// <summary>
        /// Adds custom services to the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection instance to which services are added.</param>
        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddTransient<IWeatherService, WeatherService>();
           
        }

        /// <summary>
        /// Configures GraphQL services in the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection instance to which GraphQL services are added.</param>
        public static void AddCustomGraphQLServices(this IServiceCollection services)
        {
            // GraphQL services
            services.AddScoped<IServiceProvider>(c => new FuncServiceProvider(type => c.GetRequiredService(type)));
          
            services.AddGraphQLServer().AddQueryType<QueryType>();
         
        }
    }
}
