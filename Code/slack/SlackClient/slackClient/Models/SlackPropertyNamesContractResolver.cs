using Newtonsoft.Json.Serialization;

namespace SlackClient.Models
{
    /// <summary>
    /// Converts string to a camel case
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.Serialization.DefaultContractResolver" />
    public class SlackPropertyNamesContractResolver : DefaultContractResolver
    {
        public SlackPropertyNamesContractResolver() : base()
        {
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            return SnakeCaseUtils.ToSnakeCase(propertyName);
        }
    }
}
