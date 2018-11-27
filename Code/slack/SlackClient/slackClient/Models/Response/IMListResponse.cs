using System.Collections.Generic;
using Newtonsoft.Json;
using SlackClient.Models.Types;

namespace SlackClient.Models.Response
{
    public class IMListResponse : SlackResponse
    {
        /// <summary>
        /// Gets or sets the list of IMs channels of the current workspace
        /// </summary>
        [JsonProperty(PropertyName = "Ims")]
        public IEnumerable<IMChannel> Channels { get; set; }
    }
}
