namespace SlackClient.Models.Response
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Types;

    public class SlackResponse
    {
        public bool Ok { get; set; }
        public Error Error { get; set; }
        
        [JsonExtensionData]
        public IDictionary<string, JToken> ExtensionData = new Dictionary<string, JToken>();
    }
}
