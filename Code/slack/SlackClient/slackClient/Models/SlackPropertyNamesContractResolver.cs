using Newtonsoft.Json.Serialization;

namespace SlackClient.Models
{
    public class SlackPropertyNamesContractResolver : DefaultContractResolver
    {
        public SlackPropertyNamesContractResolver()
          : base()
        {
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            return SnakeCaseUtils.ToSnakeCase(propertyName);
        }
    }
}
