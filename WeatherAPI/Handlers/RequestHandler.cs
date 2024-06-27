using Microsoft.Extensions.Options;
using WeatherAPI.Shared.Config;

namespace WeatherAPI.Handlers
{
    /// <summary>
    /// Custom HTTP request handler for modifying outgoing requests.
    /// </summary>
    public class RequestHandler : DelegatingHandler
    {
        private readonly WeatherAPIConfiguration _configuration;

        /// <summary>
        /// Constructor for the RequestHandler class.
        /// </summary>
        /// <param name="configuration">Options for configuring the Weather API.</param>
        public RequestHandler(IOptions<WeatherAPIConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        /// <summary>
        /// Modifies the outgoing HTTP request before sending it.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task representing the asynchronous operation with the modified HTTP response message.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var uri = request.RequestUri;
            var scheme = string.IsNullOrWhiteSpace(uri.Scheme) ? "" : $"{uri.Scheme}://";
            request.RequestUri = new Uri($"{scheme}{uri.Host}{uri.LocalPath}");
            return base.SendAsync(request, cancellationToken);
        }
    }

}
