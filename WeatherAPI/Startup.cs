using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Refit;
using WeatherAPI.Config;
using WeatherAPI.Extenstion;
using WeatherAPI.Handlers;
using WeatherAPI.Resolver;
using WeatherAPI.Shared.Config;
using WeatherAPI.Shared.Interfaces;

namespace WeatherAPI
{
    /// <summary>
    /// Class to configure services and request pipeline for the application.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor to initialize Startup with configuration.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configures the services used by the application.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddCustomService();

            // Configure WeatherAPIConfiguration from appsettings.json
            services.Configure<WeatherAPIConfiguration>(_configuration.GetSection("WeatherAPI"));

            // Register WeatherAPIConfigurationValidator to validate options
            services.AddSingleton<IValidateOptions<WeatherAPIConfiguration>, WeatherAPIConfigurationValidator>();

            // Add RequestHandler as a transient service
            services.AddTransient<RequestHandler>();

            // Configure Refit client for IBOMAPIService
            services.AddRefitClient<IBOMAPIService>()
                .ConfigureHttpClient((provider, client) =>
                {
                    var configuration = provider.GetRequiredService<IOptions<WeatherAPIConfiguration>>().Value;
                    client.BaseAddress = new Uri(configuration.Uri);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                }).AddHttpMessageHandler<RequestHandler>();

            // Add custom GraphQL services
            services.AddCustomGraphQLServices();

            // Configure JSON serialization settings with CustomContractResolver for snake_case naming
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CustomContractResolver();
                });

            // Add API endpoints explorer
            services.AddEndpointsApiExplorer();

            // Configure Swagger/OpenAPI generation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "South Australia Weather Observation API", Version = "v1" });
            });
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The web hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Use developer exception page for debugging
            app.UseDeveloperExceptionPage();

            // Uncomment for Swagger UI in production
            //if (env.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            // Redirect HTTP to HTTPS
            app.UseHttpsRedirection();

            // Enable routing
            app.UseRouting();

            // Enable authorization
            app.UseAuthorization();

            // Map GraphQL endpoint
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();

                // Map other controllers
                endpoints.MapControllers();
            });

            // Enable WebSockets
            app.UseWebSockets();
        }
    }
}

