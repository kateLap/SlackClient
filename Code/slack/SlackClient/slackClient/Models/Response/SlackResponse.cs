namespace SlackClient.Models.Response
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Types;

    /// <summary>
    /// General type for all Slack responses
    /// </summary>
    public class SlackResponse
    {
        /// <summary>
        /// Will be true if Slack API request is ok
        /// </summary>
        public bool Ok { get; set; }

        /// <summary>
        /// Will be true if Slack API request is wrong
        /// </summary>
        public Error Error { get; set; }

        /// <summary>
        /// The extension data of Slack API request 
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, JToken> ExtensionData = new Dictionary<string, JToken>();
    }
}
