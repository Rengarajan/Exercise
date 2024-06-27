using Newtonsoft.Json.Serialization;

namespace WeatherAPI.Resolver
{
    /// <summary>
    /// Custom contract resolver for JSON serialization that converts property names to snake_case.
    /// </summary>
    public class CustomContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Resolves property names by converting them to snake_case if they are not already formatted as such.
        /// </summary>
        /// <param name="propertyName">The name of the property to resolve.</param>
        /// <returns>The resolved property name in snake_case.</returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            // Check if the property name is already in snake case
            if (propertyName.Contains("_"))
                return propertyName;

            // Convert the property name to snake case
            return string.Concat(propertyName.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }


}
